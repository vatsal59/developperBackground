#include "model.h"
#include "obj_loader.h"

Model::Model(const char* path) : m_vao() , m_drawcall(m_vao , 0, GL_UNSIGNED_INT)
// TODO
{
	// TODO
	// Cette fois-ci, la méthode BufferObject::allocate est publique (et devrait être utilisé ici)
	std::vector<GLfloat> vertexData;
    std::vector<GLuint> indices;

	loadObj(path , vertexData , indices);
	m_vbo.allocate(GL_ARRAY_BUFFER , vertexData.size()*sizeof(GLfloat), vertexData.data() , GL_STATIC_DRAW);
	m_ebo.allocate(GL_ELEMENT_ARRAY_BUFFER , indices.size() * sizeof(GLuint) , indices.data() , GL_STATIC_DRAW);
	std::cout << "Vertex Data size: " << vertexData.size() << std::endl;
    std::cout << "Indices size: " << indices.size() << std::endl;

	m_vao.bind();
	m_vbo.bind();
	m_ebo.bind();

	m_vao.specifyAttribute(m_vbo , 0 , 3 , 5, 0);
	m_vao.specifyAttribute(m_vbo , 1 , 2 , 5, 3);

	m_vao.unbind();

	m_drawcall.setCount(indices.size());

}

void Model::loadObj(const char* path, std::vector<GLfloat>& vertexData, std::vector<GLuint>& indices)
{
	objl::Loader loader;
	if (!loader.LoadFile(path))
	{
		std::cout << "Unable to load model " << path << std::endl;
		return;
	}

	for (const auto& vertex : loader.LoadedVertices) {
        vertexData.push_back(vertex.Position.X);
        vertexData.push_back(vertex.Position.Y);
        vertexData.push_back(vertex.Position.Z);
        vertexData.push_back(vertex.TextureCoordinate.X);
        vertexData.push_back(vertex.TextureCoordinate.Y);
    }
	indices = loader.LoadedIndices;
}

void Model::draw()
{
	// TODO
	m_vao.bind();
	m_vbo.bind();
	m_ebo.bind();
	m_drawcall.draw();
	m_vao.unbind();
}

