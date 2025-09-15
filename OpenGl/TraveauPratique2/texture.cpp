#include "texture.h"

#define STB_IMAGE_IMPLEMENTATION
#include "stb_image.h"

#include <iostream>

Texture2D::Texture2D(const char* path)
{
    int width, height, nChannels;
    stbi_set_flip_vertically_on_load(true);
    glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
    unsigned char* data = stbi_load(path, &width, &height, &nChannels, 0);
    if (data == NULL)
        std::cout << "Error loading texture \"" << path << "\": " << stbi_failure_reason() << std::endl;

    // TODO - Chargement de la texture, attention au format des pixels de l'image!
    GLenum format = (nChannels == 3) ? GL_RGB : GL_RGBA; // on verifie si le alpha est present
    glGenTextures(1, &m_id);
    glBindTexture(GL_TEXTURE_2D, m_id);
    glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);
   std :: cout << "channel: " << nChannels << std::endl;

    stbi_image_free(data);
}

Texture2D::~Texture2D()
{
    // TODO
    if(m_id)
    {
        glDeleteTextures(1, &m_id);
    }
}

void Texture2D::setFiltering(GLenum filteringMode)
{
    // TODO - min et mag filter
    if(m_id)
    {
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, filteringMode);
        glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, filteringMode);
    }
}

void Texture2D::setWrap(GLenum wrapMode)
{
    // TODO
    if(m_id)
    {
         glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, wrapMode);
         glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, wrapMode);
    }

}

void Texture2D::enableMipmap()
{
    // TODO - mipmap et filtering correspondant
    if(m_id)
    {
          glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
          glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    }
}

void Texture2D::use()
{
    // TODO
    if(m_id)
    {
          glActiveTexture(GL_TEXTURE0);
          glBindTexture(GL_TEXTURE_2D, m_id);

    }
}

