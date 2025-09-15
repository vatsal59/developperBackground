#include "buffer_object.h"
#include "utils.h"

BufferObject::BufferObject()
{
    // TODO
    glGenBuffers(1 , &m_id);
    if (m_id == 0)
    {
        return;  
    }
    m_type = GL_ARRAY_BUFFER;
    bind();
    
}

BufferObject::BufferObject(GLenum type, GLsizeiptr dataSize, const void* data, GLenum usage)
: BufferObject()
{
    // TODO
      glGenBuffers(1 , &m_id);
      if (m_id == 0)
      {
            return;  
      }
      m_type = type;
      bind();
      glBufferData(m_type, dataSize, data, usage);
}

BufferObject::~BufferObject()
{
    // TODO
    if (m_id != 0) {
        glDeleteBuffers(1, &m_id);
        m_id = 0; 
    }
}

void BufferObject::bind()
{
    // TODO
    if (m_id != 0) {
        glBindBuffer(m_type, m_id);
    }
}

void BufferObject::allocate(GLenum type, GLsizeiptr dataSize, const void* data, GLenum usage)
{
    // TODO
    if(m_id != 0)
    {
        bind();
        glBufferData(m_type, dataSize, data, usage);
    }
}

void BufferObject::update(GLsizeiptr dataSize, const void* data)
{
    // TODO
     if (m_id != 0) {
        bind();
        glBufferSubData(m_type, 0, dataSize, data);
    }
}

void* BufferObject::mapBuffer()
{
    // TODO
    if(m_id == 0)
    {
        return nullptr;
    }
    bind();
    void* bufferPtr = glMapBuffer(m_type , GL_READ_WRITE);
    if(bufferPtr == nullptr)
    {
        return nullptr;
    }
    return bufferPtr;
}

void BufferObject::unmapBuffer()
{
    // TODO
    if(m_id != 0)
    {
        bind();
        if(glUnmapBuffer(m_type) == GL_FALSE)
        {
            return;
        }

    }
}

