/*******************************************************************************
Copyright © 2015-2022 PICO Technology Co., Ltd.All rights reserved.

NOTICE：All information contained herein is, and remains the property of
PICO Technology Co., Ltd. The intellectual and technical concepts
contained herein are proprietary to PICO Technology Co., Ltd. and may be
covered by patents, patents in process, and are protected by trade secret or
copyright law. Dissemination of this information or reproduction of this
material is strictly forbidden unless prior written permission is obtained from
PICO Technology Co., Ltd.
*******************************************************************************/

using System;
using Pico.Platform.Models;
using UnityEngine;

namespace Pico.Platform
{
    /**
     * \ingroup Platform
     *
     * You can diversify user experience and grow your revenue by selling
     * products such as cosmetics, props, and coins/diamonds within your
     * app. The PICO Unity Integration SDK provides In-App Purchase (IAP)
     * service which enables users to purchase products within your app.
     * The IAP service packages a series of payments systems such as Alipay,
     * bank card, and Paypal, thereby providing you with a one-stop
     * multi-payment-method solution.
     */
    public static class IAPService
    {
        /// <summary>
        /// Records the order fulfillment result for a consumable.
        /// @note Users are unable to repurchase the same comsumable until the previous order is fulfilled.
        /// </summary>
        /// <param name="sku">The SKU of the add-on to fulfill.</param>
        public static Task ConsumePurchase(string sku)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.NotInitializedError);
                return null;
            }

            return new Task(CLIB.ppf_IAP_ConsumePurchase(sku));
        }

        /// <summary>Gets a list of purchasable add-ons in the current app.</summary>
        /// <param name="skus">The SKUs of the add-ons to retrieve. If this parameter is empty, all purchasable add-ons will be returned.</param>
        /// <returns>A list of purchasable add-ons.</returns>
        public static Task<ProductList> GetProductsBySKU(string[] skus)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.NotInitializedError);
                return null;
            }

            if (skus == null)
            {
                skus = Array.Empty<string>();
            }

            return new Task<ProductList>(CLIB.ppf_IAP_GetProductsBySKU(skus));
        }

        /// <summary>Gets a list of purchased add-ons for a user, including durables and unfilfilled consumables.</summary>
        /// <returns>A list of the user's purchased add-ons.</returns>
        public static Task<PurchaseList> GetViewerPurchases()
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.NotInitializedError);
                return null;
            }

            return new Task<PurchaseList>(CLIB.ppf_IAP_GetViewerPurchases());
        }

        /// @deprecated LaunchCheckoutFlow(string sku,string price,string currency) can be replaced by \ref LaunchCheckoutFlow2(Product product)
        /// <summary>
        /// Launches the checkout flow for a user to make a payment.
        /// \note This method don't support subscription type product,you should
        /// use \ref LaunchCheckoutFlow2 instead.
        /// </summary>
        /// <param name="sku">The SKU of the product the user wants to purchase.</param>
        /// <param name="price">The price for the product.</param>
        /// <param name="currency">The currency of the payment.</param>
        /// <returns>Returns the purchased product if the user successfully pays the money.
        /// Otherwise the purchase will be null. You can get the failure reason from the returned error code and error message.</returns>
        [Obsolete("Please use LaunchCheckoutFlow2(Product product)", false)]
        public static Task<Purchase> LaunchCheckoutFlow(string sku, string price, string currency)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.NotInitializedError);
                return null;
            }

            return new Task<Purchase>(CLIB.ppf_IAP_LaunchCheckoutFlow(sku, price, currency));
        }

        /// <summary>
        /// Launches the checkout flow for a user to make a payment.
        /// </summary>
        /// <param name="product">The add-on info which can be acquired by \ref GetProductsBySKU. The `Product` struct
        /// contains the following:
        /// * `SKU`: The unique identifier of the add-on.
        /// * `Price`: The price of the add-on.
        /// * `Currency`: The currency of the payment.
        /// * `OuterId`: The unique identifier of a subscription period. This field is only available to subscription add-ons.
        /// </param>
        /// <returns>Returns the purchased add-on if the user successfully makes the payment.
        /// Otherwise the purchase will be null. You can get the failure reason from the returned error code and error message.
        /// </returns>
        public static Task<Purchase> LaunchCheckoutFlow2(Product product)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.NotInitializedError);
                return null;
            }

            return new Task<Purchase>(CLIB.ppf_IAP_LaunchCheckoutFlowV2(product.SKU, product.Price, product.Currency, product.OuterId));
        }

        /// <summary>
        /// Gets the next page of purchasable add-ons.
        /// </summary>
        /// <param name="list">The current page of purchasable add-ons.</param>
        /// <returns>The next page of purchasable add-ons.</returns>
        public static Task<ProductList> GetNextProductListPage(ProductList list)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.NotInitializedError);
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
        /// Gets the next page of purchased add-ons.
        /// </summary>
        /// <param name="list">The current page of purchased add-ons.</param>
        /// <returns>The next page of purchased add-ons.</returns>
        public static Task<PurchaseList> GetNextPurchaseListPage(PurchaseList list)
        {
            if (!CoreService.Initialized)
            {
                Debug.LogError(CoreService.NotInitializedError);
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