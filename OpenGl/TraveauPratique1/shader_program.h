#ifndef SHADER_PROGRAM_H
#define SHADER_PROGRAM_H

#include <GL/glew.h>

class ShaderObject;

class ShaderProgram // objet qui regroupe plusieurs shaders et les utilise ensemble
{
public:
    ShaderProgram();
    ~ShaderProgram();
    
    void use();
    void attachShaderObject(ShaderObject& s);
    void link();
    
    GLint getUniformLoc(const char* name);
    
private:
    void checkLinkingError();

private:
    GLuint m_id;
};


#endif // SHADER_PROGRAM_H
