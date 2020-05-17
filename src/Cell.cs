using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum Estado
    {
        adormecida = 0,
        viva,
        morta
    };

    [HideInInspector] public Estado estado = Estado.adormecida;
    public Sprite[] sprite_De_Estados;

    public void updateStatus(int vizinhosVivos)
    {
        if(vizinhosVivos == 1 || vizinhosVivos == 2)
        {
            if (estado == Estado.viva) return;
            estado = Estado.viva;
        }
        else if (vizinhosVivos == 3 || vizinhosVivos == 4)
        {
            estado = Estado.morta;
        }
        updateSprite();
    }

    private void updateSprite()
    {
        SpriteRenderer myRender = GetComponent<SpriteRenderer>();
        myRender.sprite = sprite_De_Estados[(int)estado];
    }

}
