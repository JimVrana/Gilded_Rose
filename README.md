# Gilded_Rose

I added a base entity class to include Id, date created and date modified to each object. Id the primary key of each.

The item class will also contain an available quantity.  This would normally be separated out into an inventory system, but for a sample application I can run tests for availability of items.  

There are two user roles, one for general users, 'Users' and one for administrators, 'Admin'.  Api calls dictate which roles are able to call them.  

The ClaimInjectorWebApplicationFactory was found at https://github.com/jabbera/aspnetcore-testing-role-handler to make testing of the roles easier to implement.

Sample Token Request:
	POST /api/Users/authenticate HTTP/1.1
	Host: localhost:44315
	Content-Type: application/json
	User-Agent: PostmanRuntime/7.13.0
	Accept: */*
	Cache-Control: no-cache
	Postman-Token: cc3170ca-89c4-489d-b7eb-f1b56591767f,af035666-9abe-4d4c-af39-3399cf3e37d3
	Host: localhost:44315
	accept-encoding: gzip, deflate
	content-length: 46
	Connection: keep-alive
	cache-control: no-cache

	{
			"username": "admin",
			"password": "admin"
	}
Sample Token response:
	{
			"id": 1,
			"firstName": "Admin",
			"lastName": "User",
			"username": "admin",
			"password": null,
			"role": "User",
			"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU1ODQ0MTg4NSwiZXhwIjoxNTU5MDQ2Njg1LCJpYXQiOjE1NTg0NDE4ODV9.uHsea3j51t8zf11P5K23wGv05WJ_266W1Ri_ZbsYeg8",
			"userName": null,
			"normalizedUserName": null,
			"email": null,
			"normalizedEmail": null,
			"emailConfirmed": false,
			"passwordHash": null,
			"securityStamp": null,
			"concurrencyStamp": "b3e15789-8854-4da5-acd0-ae6883ba5227",
			"phoneNumber": null,
			"phoneNumberConfirmed": false,
			"twoFactorEnabled": false,
			"lockoutEnd": null,
			"lockoutEnabled": false,
			"accessFailedCount": 0
	}


Sample request:
	GET /api/values HTTP/1.1
	Host: localhost:44315
	Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTU1ODQ0MTI0MywiZXhwIjoxNTU5MDQ2MDQzLCJpYXQiOjE1NTg0NDEyNDN9.Fq0OrDCZbXSTI1fGU_xh9NpVntiv4rN32W0xZqC6RbI
	User-Agent: PostmanRuntime/7.13.0
	Accept: */*
	Cache-Control: no-cache
	Postman-Token: 35824615-9a95-4e59-aecc-f1f16388924d,f5bb8e32-0b06-43a5-afab-9df8187961e1
	Host: localhost:44315
	accept-encoding: gzip, deflate
	Connection: keep-alive
	cache-control: no-cache

Sample response:
	["value1","value2"]
