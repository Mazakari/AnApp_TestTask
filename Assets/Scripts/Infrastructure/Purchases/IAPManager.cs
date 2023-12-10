using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == PurchaseConstants.PURCHASE_NAME_TICKET_500)
        {
            Debug.Log("Player got 500 tickets");
        }

        if (product.definition.id == PurchaseConstants.PURCHASE_NAME_TICKET_1200)
        {
            Debug.Log("Player got 1200 tickets");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"{product.definition.id} failed because of {failureReason}");
    }
}
