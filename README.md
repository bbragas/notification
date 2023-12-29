# Notification Api

## Build

[![Build Status](https://vegait.visualstudio.com/Notification/_apis/build/status/Application/Api/notification.api?branchName=main)](https://vegait.visualstudio.com/Notification/_build/latest?definitionId=93&branchName=main)

# APIs

## Blacklist

___
`PUT /api/v1/Blacklist`

<details>
  <summary>Create or update a blacklist item.</summary>

| Parameter       | Type   | Mandatory | Description                                                                                 |
|-----------------|--------|-----------|---------------------------------------------------------------------------------------------|
| ProjectId       | Guid   | true      | Identifies the external project.                                                            |
| ProjectEntityId | Guid   | false     | Identifies the branch of a external project.                                                |
| Contact         | String | true      | Indicates the customer contact to which the notification will be sent(email, phone number). |
| Description     | String | false     | A description for the blacklist.                                                            |

### Response
Status: 204

</details>


___
`DELETE /api/v1/Blacklist/{blacklistItemId}`
<details>
  <summary>Delete a blacklist item.</summary>

### Request - Path parameter
| Parameter         | Type   | Mandatory | Description                      |
|-------------------|--------|-----------|----------------------------------|
| blacklistItemId   | Guid   | true      | Indicates the blacklist item id. |

### Response
Status: 204

</details>

___
`GET /api/v1/Blacklist/list`
<details>
  <summary>
  Get all blacklist items by filter.<br/>
  If none of the parameters are informed, all blacklist records will be returned, following the default pagination.<br/>
  If the ProjectId parameter is informed and ProjectEntityId is not informed, all records of the ProjectId will be returned, both records that have a ProjectEntityId value and those that do not.
  </summary>

### Request - Query string

| Parameter       | Type | Deault Value | Min Value | Max Value    | Description                                     |
|-----------------|------|--------------|-----------|--------------|-------------------------------------------------|
| ProjectId       | Guid | null         | -         | -            | Identifies the external project.                |
| ProjectEntityId | Guid | null         | -         | -            | Identifies the branch of a external project.    |
| PerPage         | Int  | 50           | 1         | 100          | Indicates the maximum number of items per page. |
| Page            | Int  | 1            | 1         | int.MaxValue | Indicates the page that you want to see.        |



### Response
```json
{
  "items": [
    {
      "id": "3c3c4a74-93d6-44fe-82d6-617636d6e7a3",
      "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "productEntityId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "contact": "test1@test.com",
      "description": "string"
    }
  ],
  "total": 2
}
```
</details>

## Email

---
`POST /api/v1/Email`
<details>
  <summary>Create an email notification.</summary>

### Request - Body

| Parameter       | Type     | Mandatory | Description                                                                                |
|-----------------|----------|-----------|--------------------------------------------------------------------------------------------|
| ProjectId       | Guid     | true      | Identifies the external project.                                                           |
| ProjectEntityId | Guid     | true      | Identifies the branch of a external project.                                               |
| Body            | String   | true      | Indicates the email body message. It supports the HTML format.                             |
| Client          | String   | true      | Indicates the VEGA product identifier.                                                     |
| Subject         | String   | true      | Indicates the email subject.                                                               |
| Campaign        | String   | true      | Indicates the campaign identifier.                                                         |
| SenderName      | String   | true      | Indicates the sender name.                                                                 |
| SenderEmail     | String   | true      | Indicates the sender email.                                                                |
| RecipientName   | String   | true      | Indicates the recipient name.                                                              |
| RecipientEmail  | String   | true      | Indicates the recipient email.                                                             |
| ScheduledTo     | DateTime | false     | Indicates the time when the notification should be fired. If null it is fired immediately. |

### Response
Status: 204

</details>


---
`POST /api/v1/Email/batch`
<details>
  <summary>Create an email notification in batch.</summary>

### Request - Body

| Parameter       | Type        | Mandatory | Description                                                                                |
|-----------------|-------------|-----------|--------------------------------------------------------------------------------------------|
| ProjectId       | Guid        | true      | Identifies the  external project.                                                          |
| Body            | String      | true      | Indicates the email body message. It supports the HTML format.                             |
| Client          | String      | true      | Indicates the VEGA product identifier.                                                     |
| Subject         | String      | true      | Indicates the email subject.                                                               |
| Campaign        | String      | true      | Indicates the campaign identifier.                                                         |
| SenderName      | String      | true      | Indicates the sender name.                                                                 |
| SenderEmail     | String      | true      | Indicates the sender email.                                                                |
| Customers       | Customers[] | true      | Indicates the customer receiver data.                                                      |
| ScheduledTo     | DateTime    | false     | Indicates the time when the notification should be fired. If null it is fired immediately. |

### Customer
| Parameter       | Type       | Mandatory | Description                                  |
|-----------------|------------|-----------|----------------------------------------------|
| ProjectEntityId | Guid       | true      | Identifies the branch of a external project. |
| RecipientName   | String     | true      | Indicates the recipient name.                |
| RecipientEmail  | String     | true      | Indicates the recipient email.               |
| Attributes      | Dictionary | false     | Additional custom data.                      |

### Response
Status: 202

</details>


## Sms

---
`POST /api/v1/Sms`
<details>
  <summary>Create a sms notification.</summary>

### Request - Body

| Parameter        | Type     | Mandatory | Description                                                                                   |
|------------------|----------|-----------|-----------------------------------------------------------------------------------------------|
| ProjectId        | Guid     | true      | Identifies the external project.                                                              |
| ProjectEntityId  | Guid     | true      | Identifies the branch of a external project.                                                  |
| Message          | String   | true      | Indicates the sms body message. It supports the HTML format.                                  |
| Client           | String   | true      | Indicates the VEGA product identifier.                                                        |
| Campaign         | String   | true      | Indicates the campaign identifier.                                                            |
| SenderName       | String   | false     | Indicates the sender name.                                                                    |
| PhoneNumber      | String   | true      | Indicates the phone whitch will receive the sms message.                                      |
| ScheduledTo      | DateTime | false     | Indicates the time when the notification should be fired. If null it is fired immediately.   |

### Response
Status: 204

</details>

## Report
---
`GET /api/v1/Report/Sms`
<details>
  <summary>Get Sms Report of all sms notifications by filter</summary>

### Request - Query string

| Parameter       | Type     | Deault Value                | Min Value          | Max Value          | Description                       |
|-----------------|----------|-----------------------------|--------------------|--------------------|-----------------------------------|
| ProjectIds      | Guid[]   | -                           | -                  | -                  | Identifies the external projects. |
| From            | DateTime | First day of current month  | DateTime.MinValue  | DateTime.MaxValue  | Initial date time range.          |
| To              | DateTime | First day of next month     | DateTime.MinValue  | DateTime.MaxValue  | Final date time range.            |

### Response
Status: 200

</details>

## Whatsapp
---
`POST /api/v2/Whatsapp`
<details>
  <summary>Create whatsapp notification</summary>

### Request - Query string

| Parameter       | Type     | Mandatory | Description                                                                                                                  |
|-----------------|----------|-----------|-----------------------------------                                                                                           |
| UnitId          | Guid     | Yes       | Identifies a VEGA IT's unit id. |
| ProjectId       | Guid     | Yes       | Identifies a VEGA IT's product id. This id must have to be the same in all notifications requests from this product.         |
| ProjectEntityId | Guid     | No        | Identifies a section inside a VEGA IT's project. Example a ProjectEntityId Guid-A for marketing messages and a ProjectEntityId Guid-B for messages regarding customer account security.                                                                                                                                                       |
| ProjectName     | String   | Yes       | Identifies a VEGA IT's product name. Example: HXP, Feedback                                                                  |
| Message         | String   | Yes       | Notification's body.                                                                                                         |
| MessageType     | Int      | Yes       | Text=0, Link=1, Media=2, Base64Media=3                                                                                       |
| Subtitle        | String   | No        | Notification's subtitle description.                                                                                         |
| RecipientNumber | String   | Yes       | Identifies the phone whitch will receive the whatsapp notification.                                                          |
| ExternalId      | String   | No        | A externalId that helps to identify the notification. This Id will be in the events triggered by the application             |
| ScheduledTo     | DateTime | Yes       | Indicates the time when the notification should be fired. If null it is fired immediately.                                   |

### Response
```json
{
  "id": "3c3c4a74-93d6-44fe-82d6-617636d6e7a3"
}
```
Status: 200

#### Examples
Text Notification
```json
{
  "unitId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projecttId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectEntityId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectName": "Feedback",
  "message": "Text Message",
  "messageType": 0,
  "recipientNumber": "5521969661122",
  "externalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

Link Notification
```json
{
  "unitId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projecttId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectEntityId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectName": "Feedback",
  "message": "http://vegait.com/",
  "messageType": 1,
  "subtitle": "Vega IT link",
  "recipientNumber": "5521969661122",
  "externalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

UrlMedia Notification
```json
{
  "unitId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projecttId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectEntityId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectName": "Feedback",
  "message": "http://vegait.com/Assets/img/Banner/bg-hxp.jpg",
  "messageType": 2,
  "subtitle": "Vega IT url media",
  "recipientNumber": "5521969661122",
  "externalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

Base64Media Notification
```json
{
  "unitId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projecttId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectEntityId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "projectName": "Feedback",
  "message": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAA0JCgsKCA0LCgsODg0PEyAVExISEyccHhcgLikxMC4pLSwzOko+MzZGNywtQFdBRkxOUlNSMj5aYVpQYEpRUk//==",
  "messageType": 3,
  "subtitle": "Vega IT image with base64",
  "recipientNumber": "5521969661122",
  "externalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
</details>


---
`GET /api/v2/Whatsapp/Queue`
<details>
  <summary>Get the count of all whatsapp notification stopped at the provider queue </summary>


### Response
```json
{
  "sending": 0
}
```
Status: 200

</details>