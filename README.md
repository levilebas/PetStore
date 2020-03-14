# PetStore
PetStore Technical Test

### Prequisites 
1. Visual Studio 2019 (https://visualstudio.microsoft.com/downloads/)
2. .Net Core 3.1 SDK (https://dotnet.microsoft.com/download/dotnet-core/3.1)
3. Access to the PetStore API https://petstore.swagger.io/

### Instructions
1. Clone this repository (https://github.com/levilebas/PetStore)

2. *Right click* on the solution and click on *Properties*

3. Ensure Unify.PetStore.Test is set as the startup project.

4. Run the solution (*Press F5*) by clicking on the menu *Debug* > *Start Debugging*

4. View Results 
```
Uncategorized
--PetNameD
Category 1 Name
--PetNameA
--PetNameC
Category 2 Name
--PetNameB

```

### Notes
1. PetStoreClient Generated using NSwag Studio https://github.com/RicoSuter/NSwag/wiki/NSwagStudio and PetStore swagger json https://petstore.swagger.io/v2/swagger.json
2. PetStoreClient modified with
```
partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings)
{
    settings.ContractResolver = new SafeContractResolver();
}

class SafeContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
{
    protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(System.Reflection.MemberInfo member, Newtonsoft.Json.MemberSerialization memberSerialization)
    {
        var jsonProp = base.CreateProperty(member, memberSerialization);
        jsonProp.Required = Newtonsoft.Json.Required.Default;
        return jsonProp;
    }
}
```
3. PetStoreClient modified by adding a default category * = new Category { Id = 0, Name = "Uncategorized"};*

### TODO
1. Extract printing and ordering functionality from Program.cs so that it can be tested.
