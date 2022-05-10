This is a Web API project built in .NET core 3.1 that calculates the insurance amount for a particular product or multiple products in an order. Build the solution and run it. Browse to http://localhost:5001/. You should be greeted with a swagger page with all the endpoints exposed.

## Notable functions
`CalculateInsurance(InsuranceDto insuranceDto);`

`CalculateInsurance(OrderDto orderDto);`

# Requirements
There is an existing endpoint that, given the information about the product, calculates the total cost of insurance according to the rules below:
  - If the product sales price is less than € 500, no insurance required
  - If the product sales price=> € 500 but < € 2000, insurance cost is € 1000
  - If the product sales price=> € 2000, insurance cost is €2000
  - If the type of the product is a smartphone or a laptop, add € 500 more to the insurance cost.


# Task #1 
## Bug-Fix
The financial manager reported that when customers buy a laptop that costs less than € 500, insurance is not calculated, while it should be € 500.

There was a slight error. The if loop that checks whether we should add insurance amount for
smartphones or laptops was inside another else condition while it should have been
outside of that if-else block.

# Task #2 
## REFACTORING
It looks like the already implemented functionality has some quality issues. Refactor that code, but be sure to maintain the same behavior. 

1) I have used design pattern.
2) `BusinessRules.cs` has been decomposed into three independent services.
3) `ProductService.cs` is concerned with only getting the details of a product.
4) `ProductTypeService.cs` is concerned with only getting the details of a product type.
5) `InsuranceService.cs` is concerned with only calculating the insurance for a particular product
or an order probably with/without surcharge.
6) Models shared among different services are moved into a seperate project for easy sharing.
7) I have used DI to inject my services into InsuranceController.

# Task #3 
Now we want to calculate the insurance cost for an order and for this, we are going to provide all the products that are in a shopping cart.

## Order
I have added a new method `CalculateInsuranceForOrder(OrderDto orderDto)` which calculates the total insurance value for all of the products in an order.
The request parameter and the response will look like this :-
```json
{
  "orderId": 0,
  "orders": [
    {
      "insuranceDto": {
        "productId": 0,
        "insuranceValue": 0,
        "productTypeName": "string",
        "productTypeHasInsurance": true,
        "salesPrice": 0,
        "productTypeId": 0,
        "surchargeRate": true
      },
      "quantity": 0
    }
  ],
  "insuranceAmount": 0
}
```

An order contains `orderId`, orders denoting the list of products denoted by a list of `insuranceDtos`, a `quantity` field representing the quantity of each product and `insuranceAmount` denoting the total insurance amount for that order.

# Task #4 
We want to change the logic around the insurance calculation. We received a report from our business analysts that digital cameras are getting lost more than usual. Therefore, if an order has one or more digital cameras, add € 500 to the insured value of the order.

## Adding 500 euros to the order in case an order contains one or more digital cameras
For this, we will simply update the logic in our `InsuranceController` to add the extra amount in case we have one or more than one digital cameras.

# Task #5
As a part of this story we need to provide the administrators/back office staff with a new endpoint that will allow them to upload surcharge rates per product type. This surcharge will then  need to be added to the overall insurance value for the product type.

Please be aware that this endpoint is going to be used simultaneously by multiple users.

## Exposing APIs to update surcharge

## Assumptions:-
Since surcharge rate is associated with a product type, it shoudl fall upon the
`ProductTypeService.cs` to update that. Product Type will have an additional property to denote the surcharge rate. So, we assume that there is an endpoint listening at
http://localhost:5002/product_types/surcharge and will receive the request as

```json
{
    "productTypeId" : 21,
    "surcharge" : 500
}
```
and will update the surcharge rate of the product type and will response like
true or false accordingly. We also assume that the endpoint above is concurrent and consistent.

Now that we have the endpoint available to post the surcharges, we can automatically
calculate the insurance amount including the surcharge amount by passing a boolean value
indicating whether surcharge should be added or not.
`InsuranceService.cs` will contain both the logic for calculating the insurance amount with/without surcharge. The surcharge will be denoted by a property in `InsuranceDto` which represents a particular product input with all the required details.

We won't be able to validate this obviosuly since this endpoint does not exist yet.


# Assumptions for Validations :- 
1) A lot of validation logic has been skipped to keep the code simple.
2) We assume that if `canBeInsured` is false for a product, the insurance amount for that product is 0.
3) We assume that in case of an invalid product id, we return 404 and 400 in case of a bad request and 500 in case of any other exceptions.
4) We assume there is no limit on the number of products that can be in an order.
5) We assume an order will always contain valid productIds.
6) Some of the comparison logic has been changed for brevity. Rather than comparing with their names, we compare a product type with their corresponding ids as the names may change in future. Whenever appropriate, comments have been added to explain what we are doing in the code.
