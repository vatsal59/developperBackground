#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 velocity;
layout (location = 2) in vec4 color;
layout (location = 3) in vec2 size;
layout (location = 4) in float timeToLive;

out vec3 positionMod;
out vec3 velocityMod;
out vec4 colorMod;
out vec2 sizeMod;
out float timeToLiveMod;

uniform float time;
uniform float dt;

uint seed = uint(time * 1000.0) + uint(gl_VertexID);
uint randhash( ) // entre  0 et UINT_MAX
{
    uint i=((seed++)^12345391u)*2654435769u;
    i ^= (i<<6u)^(i>>26u); i *= 2654435769u; i += (i<<5u)^(i>>12u);
    return i;
}
float random() // entre  0 et 1
{
    const float UINT_MAX = 4294967295.0;
    return float(randhash()) / UINT_MAX;
}

const float PI = 3.14159265359f;
vec3 randomInCircle(in float radius, in float height)
{
    float r = radius * sqrt(random());
    float theta = random() * 2 * PI;
    return vec3(r * cos(theta), height, r * sin(theta));
}


const float MIN_TIME_TO_LIVE = 1.7f;
const float MAX_TIME_TO_LIVE = 2.0f;
const float INITIAL_RADIUS = 0.2f;
const float INITIAL_HEIGHT = 0.0f;
const float FINAL_RADIUS = 0.5f;
const float FINAL_HEIGHT = 5.0f;

const float INITIAL_SPEED_MIN = 0.5f;
const float INITIAL_SPEED_MAX = 0.6f;

const float INITIAL_ALPHA = 0.0f;
const float ALPHA = 0.1f;
const vec3 YELLOW_COLOR = vec3(1.0f, 0.9f, 0.0f);
const vec3 ORANGE_COLOR = vec3(1.0f, 0.4f, 0.2f);
const vec3 DARK_RED_COLOR = vec3(0.1, 0.0, 0.0);

const vec3 ACCELERATION = vec3(0.0f, 0.1f, 0.0f);

void main()
{
    if (timeToLive < 0.0) {
        vec3 initialPos = randomInCircle(INITIAL_RADIUS, INITIAL_HEIGHT);
        vec3 targetPos = randomInCircle(FINAL_RADIUS, FINAL_HEIGHT);
        vec3 direction = normalize(targetPos - initialPos);
        float speed = INITIAL_SPEED_MIN + random() * (INITIAL_SPEED_MAX - INITIAL_SPEED_MIN);

        positionMod = initialPos;
        velocityMod = direction * speed;
        colorMod = vec4(YELLOW_COLOR, INITIAL_ALPHA);
        sizeMod = vec2(0.5, 1.0);
        timeToLiveMod = MIN_TIME_TO_LIVE + random() * (MAX_TIME_TO_LIVE - MIN_TIME_TO_LIVE);
    } else {
        float normalizedTTL = 1.0 - (timeToLive / MAX_TIME_TO_LIVE);
        float scaleFactor = mix(1.0, 1.5, normalizedTTL);

        vec3 updatedPos = position + velocity * dt;
        vec3 updatedVel = velocity + ACCELERATION * dt;

        // Alpha calculation
        float alphaIncrease = smoothstep(0.0, 0.2, normalizedTTL);
        float alphaDecrease = smoothstep(0.8, 1.0, normalizedTTL);
        float finalAlpha = (alphaIncrease * (1.0 - alphaDecrease)) * ALPHA;

        // Color calculation
        vec4 finalColor;
        if (normalizedTTL <= 0.25) {
            finalColor = vec4(YELLOW_COLOR, finalAlpha);
        } else if (normalizedTTL <= 0.3) {
            vec3 blended = mix(YELLOW_COLOR, ORANGE_COLOR, smoothstep(0.25, 0.3, normalizedTTL));
            finalColor = vec4(blended, finalAlpha);
        } else if (normalizedTTL <= 0.5) {
            finalColor = vec4(ORANGE_COLOR, finalAlpha);
        } else if (normalizedTTL <= 1.0) {
            vec3 blended = mix(ORANGE_COLOR, DARK_RED_COLOR, smoothstep(0.5, 1.0, normalizedTTL));
            finalColor = vec4(blended, finalAlpha);
        } else {
            finalColor = vec4(ORANGE_COLOR, finalAlpha);
        }

        positionMod = updatedPos;
        velocityMod = updatedVel;
        colorMod = finalColor;
        sizeMod = vec2(0.5, 1.0) * scaleFactor;
        timeToLiveMod = timeToLive - dt;
    }
}
