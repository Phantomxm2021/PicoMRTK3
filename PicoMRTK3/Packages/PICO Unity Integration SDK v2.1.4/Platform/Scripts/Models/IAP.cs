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

namespace Pico.Platform.Models
{
    /**
     * \ingroup Models
     */
    /// <summary>
    /// The add-on that can be purchased in the app.
    ///
    /// You can create in-app products on the PICO Developer Platform.
    /// </summary>
    public class Product
    {
        /**@brief The description of the add-on. */
        public readonly string Description;

        /**@brief The detailed description of the add-on. */
        public readonly string DetailDescription;

        /**@brief The price of the add-on, which is a number string. */
        public readonly string Price;

        /**@brief The currency required for purchasing the add-on. */
        public readonly string Currency;

        /**@brief The name of the add-on. */
        public readonly string Name;

        /**@brief The unique identifier of the add-on. */
        public readonly string SKU;

        /**@brief The icon of the add-on, which is an image URL.*/
        public readonly string Icon;

        /**@brief The type of the add-on */
        public readonly AddonsType AddonsType;

        /**@brief The period type for the subscription add-on.*/
        public readonly PeriodType PeriodType;

        /**@brief The trial period unit for the subscription add-on.*/
        public readonly PeriodType TrialPeriodUnit;

        /**@brief The trial period value for the subscription add-on.*/
        public readonly int TrialPeriodValue;

        /**@brief The original price of the add-on. This field means the price
         * without discount.
         */
        public readonly string OriginalPrice;

        /**@brief The unique identifier of a subscription period.*/
        public readonly string OuterId;

        /**@brief Whether the subscription is auto renewed.*/
        public readonly bool IsContinuous;

        public Product(IntPtr o)
        {
            Description = CLIB.ppf_Product_GetDescription(o);
            DetailDescription = CLIB.ppf_Product_GetDetailDescription(o);
            Price = CLIB.ppf_Product_GetPrice(o);
            Currency = CLIB.ppf_Product_GetCurrency(o);
            Name = CLIB.ppf_Product_GetName(o);
            SKU = CLIB.ppf_Product_GetSKU(o);
            Icon = CLIB.ppf_Product_GetIcon(o);
            AddonsType = CLIB.ppf_Product_GetAddonsType(o);
            PeriodType = CLIB.ppf_Product_GetPeriodType(o);
            TrialPeriodUnit = CLIB.ppf_Product_GetTrialPeriodUnit(o);
            TrialPeriodValue = CLIB.ppf_Product_GetTrialPeriodValue(o);
            OuterId = CLIB.ppf_Product_GetOuterId(o);
            OriginalPrice = CLIB.ppf_Product_GetOriginalPrice(o);
            IsContinuous = CLIB.ppf_Product_IsContinuous(o);
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

    /**
     * \ingroup Models
     */
    /// <summary>
    /// The add-on that the current user has purchased.
    /// </summary>
    public class Purchase
    {
        /**@brief The expiration time. Only valid when it's subscription type.*/
        public readonly DateTime ExpirationTime;

        /**@brief The grant time. Only valid when it's subscription type.*/
        public readonly DateTime GrantTime;

        /** @brief The ID of the purchase order. */
        public readonly string ID;

        /** @brief The unique identifier of the add-on in the purchase order. */
        public readonly string SKU;

        /** @brief The icon of the add-on.*/
        public readonly string Icon;

        /** @brief The type of the purchased add-on.*/
        public readonly AddonsType AddonsType;

        /** @brief The outer id of the purchased add-on.*/
        public readonly string OuterId;

        /** @brief The current period type of subscription. Only valid when it's subscription.*/
        public readonly PeriodType CurrentPeriodType;

        /** @brief The next period type of subscription. Only valid when it's subscription.*/
        public readonly PeriodType NextPeriodType;

        /** @brief The next pay time of subscription. Only valid when it's subscription.*/
        public readonly DateTime NextPayTime;

        /**@brief The discount info of the purchase.*/
        public readonly DiscountType DiscountType;

        public Purchase(IntPtr o)
        {
            ExpirationTime = TimeUtil.MilliSecondsToDateTime(CLIB.ppf_Purchase_GetExpirationTime(o));
            GrantTime = TimeUtil.MilliSecondsToDateTime(CLIB.ppf_Purchase_GetGrantTime(o));
            ID = CLIB.ppf_Purchase_GetID(o);
            SKU = CLIB.ppf_Purchase_GetSKU(o);
            Icon = CLIB.ppf_Purchase_GetIcon(o);
            AddonsType = CLIB.ppf_Purchase_GetAddonsType(o);
            OuterId = CLIB.ppf_Purchase_GetOuterId(o);
            CurrentPeriodType = CLIB.ppf_Purchase_GetCurrentPeriodType(o);
            NextPeriodType = CLIB.ppf_Purchase_GetNextPeriodType(o);
            NextPayTime = TimeUtil.MilliSecondsToDateTime(CLIB.ppf_Purchase_GetNextPayTime(o));
            DiscountType = CLIB.ppf_Purchase_GetDiscountType(o);
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