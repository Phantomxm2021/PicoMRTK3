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

namespace Pico.Platform.Models
{
    /// <summary>
    /// The product that can by bought in the app.
    ///
    /// You can configure In-App products in the developer center.
    /// </summary>
    public class Product
    {
        /**@brief The description of the product.*/
        public readonly string Description;

        /**@brief The price of the product,it's a number string.*/
        public readonly string Price;

        /**@brief The currency of the product,it's the currency string.*/
        public readonly string Currency;

        /**@brief The name of the product*/
        public readonly string Name;

        /**@brief The identifier of the product*/
        public readonly string SKU;

        public Product(IntPtr o)
        {
            Description = CLIB.ppf_Product_GetDescription(o);
            Price = CLIB.ppf_Product_GetPrice(o);
            Currency = CLIB.ppf_Product_GetCurrency(o);
            Name = CLIB.ppf_Product_GetName(o);
            SKU = CLIB.ppf_Product_GetSKU(o);
        }
    }


    public class ProductList : MessageArray<Product>
    {
        public ProductList(IntPtr a)
        {
            var count = (int) CLIB.ppf_ProductArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new Product(CLIB.ppf_ProductArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_ProductArray_GetNextPageParam(a);
        }
    }

    /// <summary>
    /// The purchased product of current user.
    /// </summary>
    public class Purchase
    {
        public readonly DateTime ExpirationTime;
        public readonly DateTime GrantTime;

        /**@brief The ID of the purchase.*/
        public readonly string ID;

        /**@brief The sku of the purchase.It's the unique identifier of the product.*/
        public readonly string SKU;

        public Purchase(IntPtr o)
        {
            ExpirationTime = Util.SecondsToDateTime(CLIB.ppf_Purchase_GetExpirationTime(o));
            GrantTime = Util.SecondsToDateTime(CLIB.ppf_Purchase_GetGrantTime(o));
            ID = CLIB.ppf_Purchase_GetID(o);
            SKU = CLIB.ppf_Purchase_GetSKU(o);
        }
    }


    public class PurchaseList : MessageArray<Purchase>
    {
        public PurchaseList(IntPtr a)
        {
            var count = (int) CLIB.ppf_PurchaseArray_GetSize(a);
            this.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                this.Add(new Purchase(CLIB.ppf_PurchaseArray_GetElement(a, (UIntPtr) i)));
            }

            NextPageParam = CLIB.ppf_PurchaseArray_GetNextPageParam(a);
        }
    }
}
#endif