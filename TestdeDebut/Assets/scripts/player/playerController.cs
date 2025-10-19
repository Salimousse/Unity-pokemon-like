using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;   // Vitesse du joueur
    private bool isMoving;         // Empêche le joueur de bouger en plein déplacement
    private Vector2 input;         // Stocke la direction du mouvement

    private void Update()
    {
        // Si déjà en déplacement, ne rien faire
        if (isMoving)
            return;

        // Récupère l'entrée du joueur
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Pour éviter les diagonales (comme Pokémon)
        if (input.x != 0) input.y = 0;

        // Si le joueur appuie sur une touche
        if (input != Vector2.zero)
        {
            // Calcule la position cible
            var targetPos = transform.position + new Vector3(input.x, input.y, 0);

            // Lance le déplacement
            StartCoroutine(Move(targetPos));
        }
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        // Déplacement fluide jusqu’à la case suivante
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}
