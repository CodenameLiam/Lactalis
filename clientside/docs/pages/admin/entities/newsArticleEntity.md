# NewsArticleEntity Entity

Details regarding a given NewsArticleEntity.

## Special Attributes

| Name   | Description                                                                     |
| ------ | ------------------------------------------------------------------------------- |
| CRUD   | Allows for management of data. CRUD stands for create, read, update and delete. |
| Export | Allows for data to be exported.                                                 |

## Attributes

| Name         |  Type   |               Example                |        Required         | Rules     | Description |
| ------------ | :-----: | :----------------------------------: | :---------------------: | --------- | ----------- |
| Headline     | STRING  |                Sally                 | <i class="fa fa-times"> | <ul></ul> |             |
| Description  | STRING  |                Sally                 | <i class="fa fa-times"> | <ul></ul> |             |
| FeatureImage | STRING  | 7639712d-f95a-487d-9749-d7508e827d1e | <i class="fa fa-times"> | <ul></ul> |             |
| Content      | STRING  |                Sally                 | <i class="fa fa-times"> | <ul></ul> |             |
| Qld          | BOOLEAN |                 true                 | <i class="fa fa-times"> | <ul></ul> |             |
| Nsw          | BOOLEAN |                 true                 | <i class="fa fa-times"> | <ul></ul> |             |
| Vic          | BOOLEAN |                 true                 | <i class="fa fa-times"> | <ul></ul> |             |
| Tas          | BOOLEAN |                 true                 | <i class="fa fa-times"> | <ul></ul> |             |
| Wa           | BOOLEAN |                 true                 | <i class="fa fa-times"> | <ul></ul> |             |
| Sa           | BOOLEAN |                 true                 | <i class="fa fa-times"> | <ul></ul> |             |
| Nt           | BOOLEAN |                 true                 | <i class="fa fa-times"> | <ul></ul> |             |

## Security

| Group  |         Create          |          Read           |         Update          |         Delete          |
| ------ | :---------------------: | :---------------------: | :---------------------: | :---------------------: |
| Admin  | <i class="fa fa-check"> | <i class="fa fa-check"> | <i class="fa fa-check"> | <i class="fa fa-check"> |
| Farmer | <i class="fa fa-times"> | <i class="fa fa-check"> | <i class="fa fa-times"> | <i class="fa fa-times"> |

## List of Records

The list of records provided a means to see all of the NewsArticleEntity found in the system. It is broken into pages with navigation between the pages available at the bottom of the list.

The list can be sorted based the the various attributes associated with the NewsArticleEntity Entity by clicking the columns to sort the list either ascending or desending.

Additional operations can be performed on the list such as search and filtering by making use of the functions at the top of the list. Additionally, actions can be performed against individual records (where applicable),
by making use of the buttons situated on the right of each row.

## Actions

### Create

1. To create a new NewsArticleEntity Entity entry click "Create new"
2. Fill in the fields so that they match the the rules defined above.
3. Click Submit.
