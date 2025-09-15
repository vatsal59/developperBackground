#version 330 core

// TODO

layout(location = 0) in vec3 position;  
layout(location = 1) in vec2 texCoord_in; 
uniform int index;    // Index de la sous-texture (0 à 3 pour les tasses)
uniform int isPlate;  // 0 -> tasse, 1 -> assiette
uniform mat4 mvpCup;

out vec2 texCoord; // Coordonnée UV changer pour texture

void main()
{
    int columnNum = 3;
    int text_col = 2;
    int rowsNum = 2;

    int colIndex = index % text_col; // fournis les index sur colomn dans la partie tasse
    int rowIndex = index / rowsNum;  // fournis les index sur row dans la partie assiette


    float texWidth = 1.0 / float(columnNum); // taille dune texture 
    float texHeight = 1.0 / float(rowsNum);  // hauteur dune texture


     if(isPlate == 1) // a continuer
    {
         float texWidthAssiete = texWidth / float(text_col); // taille dune texture dassiete
         float texHeightAssiete = texHeight / float(rowsNum);  // hauteur dune texture dassiete

         vec2 offsetTab = vec2(2 * texWidth, 1 * texHeight);
         vec2 offsetAssiete = vec2(colIndex * texWidthAssiete, rowIndex * texHeightAssiete);
         texCoord = texCoord_in*vec2(texWidthAssiete , texHeightAssiete) + offsetAssiete + offsetTab;
         gl_Position = mvpCup*vec4(position, 1.0);


    }else
    {
        vec2 offsetTasse = vec2(colIndex * texWidth, rowIndex * texHeight);
        texCoord = texCoord_in*vec2(texWidth , texHeight) + offsetTasse;
        gl_Position = mvpCup*vec4(position, 1.0);
    }
    
}