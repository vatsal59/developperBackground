#include "draw_commands.h"
#include "utils.h"


DrawArraysCommand::DrawArraysCommand(VertexArrayObject& vao, GLsizei count) : m_vao(vao)
{
    // TODO 
    m_vao.bind();
    m_count = count;
}

void DrawArraysCommand::draw()
{
    // TODO
    m_vao.bind();
    glDrawArrays(GL_TRIANGLES, 0, m_count);

}

void DrawArraysCommand::setCount(GLsizei count)
{
    // TODO
    m_count = count;
}

DrawElementsCommand::DrawElementsCommand(VertexArrayObject& vao, GLsizei count, GLenum type) : m_vao(vao)
{
    // TODO
    m_vao.bind();
    m_count = count;
    m_type = type;
}

void DrawElementsCommand::draw()
{
    // TODO
    m_vao.bind();
    glDrawElements( GL_TRIANGLES, m_count,m_type, 0 );
}

void DrawElementsCommand::setCount(GLsizei count)
{
    // TODO
    m_count = count;
}


