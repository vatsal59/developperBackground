#version 330 core

// TODO

in vec3 vColor;
// on envoie le out au vertex de Fragment
out vec4 color;

void main()
{
    color = vec4(vColor , 1.0);
}