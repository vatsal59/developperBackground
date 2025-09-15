#include "shader_program.h"
#include "utils.h"
#include <iostream>
#include "shader_object.h"

ShaderProgram::ShaderProgram()
{
    // TODO
    m_id = glCreateProgram();
}
    
ShaderProgram::~ShaderProgram()
{        
    // TODO
    if(m_id != 0)
    {
        glDeleteProgram(m_id);
    }
}
    
void ShaderProgram::use()
{
    // TODO
    if(m_id == 0)
    {
        return;
    }
     glUseProgram(m_id);
}
    
void ShaderProgram::attachShaderObject(ShaderObject& s)
{        
    // TODO
    if(m_id == 0)
    {
        return;
    }
    glAttachShader(m_id , s.id());
}
    
void ShaderProgram::link()
{
    // TODO
    if(m_id == 0)
    {
        return;
    }

    glLinkProgram(m_id);
    checkLinkingError();
}

GLint ShaderProgram::getUniformLoc(const char* name)
{
    // TODO
    if(m_id == 0)
    {
       return -1;
    }
     return glGetUniformLocation(m_id, name);
}
    
void ShaderProgram::checkLinkingError()
{
    GLint success;
    GLchar infoLog[1024];

    glGetProgramiv(m_id, GL_LINK_STATUS, &success);
    if (!success)
    {
        glGetProgramInfoLog(m_id, 1024, NULL, infoLog);
        glDeleteProgram(m_id);
    }
}
