#version 330 core

// TODO

layout(location = 0) in vec3 aPosition; 
layout(location = 1) in vec3 aColor;  
// Matrice 4x4 ajouter 
uniform mat4 matrice;

out vec3 vColor; // Couleur transmise au fragment shader

void main() {
    gl_Position = matrice*vec4(aPosition, 1.0); 
    vColor = aColor;                    
}
