The library is a C# wrapper for Zillow's property search API. There are some tests included to show How to use the library. 

To use this reference the ZillowLib project and update the ZillowConstants file use your zwid key instead of the place holder value. 

Sample Code to get the zestimate value:

```
 IPropertyEngine pricingEngine = new ZillowAdapter();
 var result = PricingEngine.SearchProperty("2309 Aimee Ln", "60194");
 var zestimate = PricingEngine.GetZestimate(result.zpid);
```




