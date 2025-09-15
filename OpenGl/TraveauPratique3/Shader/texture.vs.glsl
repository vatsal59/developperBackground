#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 coordTex;
// Doit avoir le nom mvp, sinon modifier resources.cpp
uniform mat4 mvp;

out vec2 texCoord;

  // TODO
void main() {
  gl_Position = mvp*vec4(aPosition, 1.0);   
  texCoord = coordTex;  
}

