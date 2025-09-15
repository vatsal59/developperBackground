#include "resources.h"

#include "utils.h"

#include "shader_object.h"

#include "vertices_data.h"

Resources::Resources() : coloredTriangleBuffer(GL_ARRAY_BUFFER, sizeof(colorTriVertices), colorTriVertices, GL_STATIC_DRAW) 
, coloredSquareReduceBuffer(GL_ARRAY_BUFFER, sizeof(colorSquareVerticesReduced), colorSquareVerticesReduced , GL_STATIC_DRAW)
, coloredSquareReduceIndicesBuffer(GL_ELEMENT_ARRAY_BUFFER , sizeof(indexes), indexes , GL_STATIC_DRAW)
{
    // TODO
    initShaderProgram(transform, "shaders/transform.vs.glsl", "shaders/transform.fs.glsl");
    initShaderProgram(basic, "shaders/basic.vs.glsl", "shaders/basic.fs.glsl");
    initShaderProgram(color, "shaders/color.vs.glsl", "shaders/color.fs.glsl");
    mvpLocation = transform.getUniformLoc("matrice");
}

void Resources::initShaderProgram(ShaderProgram& program, const char* vertexSrcPath, const char* fragmentSrcPath)    
{
    std::string vertexCode = readFile(vertexSrcPath);
    std::string fragmentCode = readFile(fragmentSrcPath);
    ShaderObject vertexShader(GL_VERTEX_SHADER , vertexCode.c_str());
    ShaderObject fragmentShader(GL_FRAGMENT_SHADER , fragmentCode.c_str());
    program.attachShaderObject(vertexShader);
    program.attachShaderObject(fragmentShader);
    program.link();
}
