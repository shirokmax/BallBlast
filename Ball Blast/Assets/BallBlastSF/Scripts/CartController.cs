using UnityEngine;

[RequireComponent(typeof(Cart), typeof(Turret))]
public class CartController : MonoBehaviour
{
    private Cart cart;
    private Turret turret;

    private bool fireEnabled;

    private void Awake()
    {
        cart = GetComponent<Cart>();
        turret = GetComponent<Turret>();

        fireEnabled = true;
    }

    private void Update()
    {
        cart.SetMovementTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButton(0) == true && fireEnabled == true)
        {
            turret.Fire();
        }
    }

    public void DisableFire()
    {
        fireEnabled = false;
    }
}