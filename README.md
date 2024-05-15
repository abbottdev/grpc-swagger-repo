Repro steps
---

`dotnet run`

open `http://localhost:5122/swagger/`


Observations
---

It appears that the generated csharp code is valid and accepts either a string enum or a numeric value for an enum and can deserialize OK.

The problem appears to be that the generated swagger makes any Enum type that is defined in a request message, a numeric enum, even if it is shared with a response contract.

If an enum is only present in a response contract, then the string enum and `WriteEnumsAsIntegers` is respected.