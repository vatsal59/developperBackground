#version 330 core

out vec4 colorOut;

uniform sampler2D texture1;

// TODO
void main()
{
    vec2 texCoords = gl_FragCoord.xy / 100.0;

    // on recupere la couleur de la position dans la texture
    vec4 texColor = texture(texture1, texCoords);

    vec3 texTint = vec3(0.25, 0.8, 1.0);

    colorOut = vec4(texColor.rgb * texTint, texColor.a);

}

