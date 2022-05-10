This is a Web API project built in .NET core 3.1 that calculates the insurance amount for a particular order or multiple products in an order.

## Notable functions
`CalculateInsurance(InsuranceDto insuranceDto);`

`CalculateInsurance(OrderDto orderDto);`


# Task #1 
## Bug-Fix

There was a slight error. The if loop that checks whether we should add insurance amount for
smartphones or laptops was inside another else condition while it should have been
outside of that if-else block.

# Task #2 
## REFACTORING
1) I have used design pattern.
2) `BusinessRules.cs` has been decomposed into three independent services.
3) `ProductService.cs` is concerned with only getting the details of a product.
4) `ProductTypeService.cs` is concerned with only getting the details of a product type.
5) `InsuranceService.cs` is concerned with only calculating the insurance for a particular product
or an order probably with/without surcharge.
6) Models shared among different services are moved into a seperate project for easy sharing.
7) I have used DI to inject my services into InsuranceController.

# Task #3 
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
## Adding 500 euros to the order in case an order contains one or more digital cameras
For this, we will simple update the logic in our controller to add the extra amount in cas we have one or more than one digital cameras.

# Task #5
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
