#include "vertex_array_object.h"
#include <iostream>
#include "utils.h"

VertexArrayObject::VertexArrayObject()
{
    // TODO
    glGenVertexArrays(1 , &m_id);
    if (m_id == 0)
    {   
        return;
    }
    bind();

}

VertexArrayObject::~VertexArrayObject()
{
    // TODO
      if(m_id != 0)
    {
        glDeleteVertexArrays(1 , &m_id);
        m_id = 0;
    }
}

void VertexArrayObject::bind()
{
    // TODO
    if(m_id != 0)
    {
         glBindVertexArray(m_id);
    }
}

void VertexArrayObject::unbind()
{
    // TODO
     if(m_id != 0)
    {
         glBindVertexArray(0);
    }
}

void VertexArrayObject::specifyAttribute(BufferObject& buffer, GLuint index, GLint size, GLsizei stride, GLsizeiptr offset)
{
    // TODO
    if(m_id != 0)
    {
        bind();
        buffer.bind();
        glVertexAttribPointer( index, size, GL_FLOAT, GL_FALSE, sizeof(GL_FLOAT)*stride, (const void*)((offset*sizeof(GL_FLOAT))));
        glEnableVertexAttribArray(index);
    }

}
