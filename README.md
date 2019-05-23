# Gilded_Rose

Assumptions:
	If this were not a test assessment, the password would be encoded as a base64 string by the calling program and then decoded by the API.
	A list of valid users is contained in the UserService.  This would normally be pulled from active directory or a database.
		Valid usernames/passwords/roles:
			admin/admin/Admin
			jvrana/test/ApiUser
			testuser/test/User
	
	

I added a base entity class to include Id, date created and date modified to each object. Id the primary key of each.

The item class will also contain an available quantity.  This would normally be separated out into an inventory system, but for a sample application I can run tests for availability of items.  

Ordering of an item checks the available quantity and if the order quantity is less than the available quantity, the order is "placed".

There are three user roles, one for general users, 'User' and one for administrators, 'Admin', one for ApiUsers, 'ApiUser'.  Api calls are not available for the 'User' role and will return Unauthorized.

The ClaimInjectorWebApplicationFactory was found at https://github.com/jabbera/aspnetcore-testing-role-handler to make testing of the roles easier to implement.

All data is transmitted in json.  

A JWT token was used to handle authentication. Each API call transmits this token, which verifies the user each time a call is made.  I had called a few web APIs in the past that used JWT, so I was most familiar with this method.

Sample Token Request:
```HTTP
POST /api/Users/authenticate HTTP/1.1
Host: localhost:44382
Content-Type: application/json
User-Agent: PostmanRuntime/7.13.0
Accept: */*
Cache-Control: no-cache
Postman-Token: fcfd15bb-33e8-426f-a55b-20abcb731438,1582a70b-1319-4a83-9737-8fd5f54791cb
Host: localhost:44382
accept-encoding: gzip, deflate
content-length: 46
Connection: keep-alive
cache-control: no-cache

{
	"username": "admin",
	"password": "admin"
}
```
Sample Token response:
```JSON
{
    "id": "1",
    "firstName": "Admin",
    "lastName": "User",
    "userName": "admin",
    "password": null,
    "role": "Admin",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1NTg2MzY3MjAsImV4cCI6MTU1ODcyMzEyMCwiaWF0IjoxNTU4NjM2NzIwfQ.2EY5iSmZiDPZlh44pSSFgttz8K-4bfm2I_s5WTc9znY",
    "normalizedUserName": null,
    "email": null,
    "normalizedEmail": null,
    "emailConfirmed": false,
    "passwordHash": null,
    "securityStamp": null,
    "concurrencyStamp": "949fd2f3-a72a-46c4-a689-4cdc9ca0adb8",
    "phoneNumber": null,
    "phoneNumberConfirmed": false,
    "twoFactorEnabled": false,
    "lockoutEnd": null,
    "lockoutEnabled": false,
    "accessFailedCount": 0
}
```

Sample request with token:
```HTTP
POST /api/Order/5/1 HTTP/1.1
Host: localhost:44382
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE1NTg2MzY3MjAsImV4cCI6MTU1ODcyMzEyMCwiaWF0IjoxNTU4NjM2NzIwfQ.2EY5iSmZiDPZlh44pSSFgttz8K-4bfm2I_s5WTc9znY
User-Agent: PostmanRuntime/7.13.0
Accept: */*
Cache-Control: no-cache
Postman-Token: 357fe52a-acc4-4cf3-80e0-a35d7d6794aa,d4568c43-69fc-40cd-906e-4d66d9eb61fc
Host: localhost:44382
accept-encoding: gzip, deflate
content-length: 
Connection: keep-alive
cache-control: no-cache
```
Sample response:
```JSON
{
    "message": "Order successfully placed"
}
```
