#include "resources.h"

#include "utils.h"


Resources::Resources()
{
    // TODO - init des shaders
    initShaderProgram(texture, "shaders/texture.vs.glsl", "shaders/texture.fs.glsl");
    mvpLocationTexture = texture.getUniformLoc("mvpTexture");
    CHECK_GL_ERROR;
    initShaderProgram(colorUniform, "shaders/colorUniform.vs.glsl", "shaders/colorUniform.fs.glsl");
    mvpLocationColorUniform = colorUniform.getUniformLoc("matrice");
    colorLocationColorUniform = colorUniform.getUniformLoc("vColor");
    initShaderProgram(cup, "shaders/cup.vs.glsl", "shaders/texture.fs.glsl");
    mvpLocationCup = cup.getUniformLoc("mvpCup");
    textureIndexLocationCup = cup.getUniformLoc("index");
    isPlateLocationCup = cup.getUniformLoc("isPlate");
}

