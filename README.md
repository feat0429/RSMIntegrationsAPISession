# RSMIntegrationsAPISession
### Requirements
- [x] Create: Implement endpoints to add new entries to the Product, ProductCategory, 
and SalesOrderHeader tables.
- [x] Read: Implement endpoints to retrieve entries from the Product, ProductCategory, 
and SalesOrderHeader tables. This should include retrieving single entries and lists of 
entries.
- [x] Update: Implement endpoints to update existing entries in the Product, ProductCategory, 
and SalesOrderHeader tables.
- [x] Implement endpoints to delete entries from the Product, ProductCategory, 
and SalesOrderHeader tables.

### AdditionalFeatures
- [x] Included JWT authentication to endpoints.

### Credentials
Username: test
<br/>
Password: 12345

### Notes
- Since only requested models are allowed, some common validations to check if related records exist, were skipped.
These validations could be made with raw SQL queries using EF Core, however I thought that this solution was far from the main goal of the challenge.
- For the same reason of the limitation of the models, some values were hardcoded. For instance, the values for the foreign keys of the
SalesOrderHeader model were hardcoded to the same value and avoid conflicts with the database. Furthermore, the authentication credentials
were hardcoded as well.

