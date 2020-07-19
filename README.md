### SAMPLE QUERY 

```
{
  legalFormType(where: { AND: { country_not: POLAND } }) {
    id
    country
    legalFormTypeNames {
      name
    }
  }
}
```

```

{
  legalFormType(
    where: { country_in: [ POLAND, GERMAN] }
  ) {
    id
    country
    legalFormTypeNames {
      name
    }
  }

}

```

```
{
  legalForm(where: { legalFormTypeId_in: [1, 2] }) {
    corporation {
      name
    }
    name
    city
    vatId
  }
}
```