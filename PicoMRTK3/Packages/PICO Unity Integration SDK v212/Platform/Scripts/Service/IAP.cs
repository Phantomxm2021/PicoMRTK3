#if PICO_INSTALL

/*******************************************************************************
Copyright © 2015-2022 Pico Technology Co., Ltd.All rights reserved.

NOTICE：All information contained herein is, and remains the property of
Pico Technology Co., Ltd. The intellectual and technical concepts
contained herein are proprietary to Pico Technology Co., Ltd. and may be
covered by patents, patents in process, and are protected by trade secret or
copyright law. Dissemination of this information or reproduction of this
material is strictly forbidden unless prior written permission is obtained from
Pico Technology Co., Ltd.
*******************************************************************************/

using System;
using Pico.Platform.Models;
using UnityEngine;

namespace Pico.Platform
{
    /**
     * \ingroup Platform
     */
    public static class IAPService
    {
        /// <summary>
        /// Records the order fulfillment result for a consumable.
        /// @note Users are unable to repurchase the same comsumable until the previous order is fulfilled.
        /// </summary>
        /// <param name="sku">The SKU of the product to fulfill.</param>
        public static Task ConsumePurchase(string sku)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.UninitializedError);
                return null;
            }

            return new Task(CLIB.ppf_IAP_ConsumePurchase(sku));
        }

        /// <summary>Gets a list of purchasable products in the current app.</summary>
        /// <param name="skus">The SKUs of the products to retrieve. If this parameter is empty, all purchasable products will be returned.</param>
        /// <returns>A list of purchasable products.</returns>
        public static Task<ProductList> GetProductsBySKU(string[] skus)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.UninitializedError);
                return null;
            }

            if (skus == null)
            {
                skus = Array.Empty<string>();
            }

            return new Task<ProductList>(CLIB.ppf_IAP_GetProductsBySKU(skus));
        }

        /// <summary>Gets a list of purchased products for a user, including products that are permanently available after one purchase and consumables that are not fulfilled yet.</summary>
        /// <returns>A list of the user's purchased products.</returns>
        public static Task<PurchaseList> GetViewerPurchases()
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.UninitializedError);
                return null;
            }

            return new Task<PurchaseList>(CLIB.ppf_IAP_GetViewerPurchases());
        }

        /// <summary>
        /// Launches the checkout flow for a user to make a payment.
        /// </summary>
        /// <param name="sku">The SKU of the product the user wants to purchase.</param>
        /// <param name="price">The price for the product.</param>
        /// <param name="currency">The currency of the payment.</param>
        /// <returns>Returns the purchased product if the user successfully pays the money.
        /// Otherwise the purchase will be null. You can get the failure reason from the returned error code and error message.</returns>
        public static Task<Purchase> LaunchCheckoutFlow(string sku, string price, string currency)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.UninitializedError);
                return null;
            }

            return new Task<Purchase>(CLIB.ppf_IAP_LaunchCheckoutFlow(sku, price, currency));
        }

        /// <summary>
        /// Gets the next page of purchasable products.
        /// </summary>
        /// <param name="list">The current page of purchasable products.</param>
        /// <returns>The next page of purchasable products.</returns>
        public static Task<ProductList> GetNextProductListPage(ProductList list)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.UninitializedError);
                return null;
            }

            if (!list.HasNextPage)
            {
                Debug.LogWarning("Pico.Platform.GetNextProductListPage: List has no next page");
                return null;
            }

            return new Task<ProductList>(
                CLIB.ppf_IAP_GetNextProductArrayPage(list.NextPageParam)
            );
        }

        /// <summary>
        /// Gets the next page of purchased products.
        /// </summary>
        /// <param name="list">The current page of purchased products.</param>
        /// <returns>The next page of purchased products.</returns>
        public static Task<PurchaseList> GetNextPurchaseListPage(PurchaseList list)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.UninitializedError);
                return null;
            }

            if (!list.HasNextPage)
            {
                Debug.LogWarning("Pico.Platform.GetNextPurchaseListPage: List has no next page");
                return null;
            }

            return new Task<PurchaseList>(CLIB.ppf_IAP_GetNextPurchaseArrayPage(list.NextPageParam));
        }
    }
}
#endif