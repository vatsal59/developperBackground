#include "shader_object.h"

#include "utils.h"
#include <iostream>

ShaderObject::ShaderObject(GLenum type, const char* code)
{
    // TODO
    m_id = glCreateShader(type);
    if(m_id == 0)
    {
        return;
    }
    glShaderSource(m_id, 1, &code, nullptr);
    glCompileShader(m_id);
    checkCompilingError();
}
    
ShaderObject::~ShaderObject()
{
    // TODO
    if(m_id != 0)
    {
        glDeleteShader(m_id);
        m_id = 0;
    }
}

GLuint ShaderObject::id()
{
    // TODO
    return m_id;

}

void ShaderObject::checkCompilingError()
{
    GLint success;
    GLchar infoLog[1024];

    glGetShaderiv(m_id, GL_COMPILE_STATUS, &success);
    if (!success)
    {
        glGetShaderInfoLog(m_id, 1024, NULL, infoLog);
        glDeleteShader(m_id);
    }
}

