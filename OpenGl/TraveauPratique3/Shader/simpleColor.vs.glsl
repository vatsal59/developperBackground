#version 330 core


layout(location = 0) in vec3 Pos;

// Doit avoir le nom "mvp", sinon modifier resources.cpp
uniform mat4 mvp;

// TODO
void main() {
    gl_Position = mvp * vec4(Pos, 1.0);
}

