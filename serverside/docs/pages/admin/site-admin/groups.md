<!--
@bot-written

WARNING AND NOTICE
Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
Full Software Licence as accepted by you before being granted access to this source code and other materials,
the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
access, download, storage, and/or use of this source code.

BOT WARNING
This file is bot-written.
Any changes out side of "protected regions" will be lost next time the bot makes any changes.
-->

# Group

Details regarding the security groups that are available in the system.

Some special groups exist that have default haviour related to them that differs from the standard group.

The special groups are,

- Admin

See the sections below for each of these for more detail on the specific behaviour.

#### Available Groups

| Name | Description | Default |
|:---: | :----: | :----: |
| Visitors | Anonymous users who have not been authenticated | false |
| Admin | Users who have been created as an Admin entity | false |
| Farmer | Users who have been created as a Farmer entity | false |

---

## Group Details



### Visitors Group 
Anonymous users who have not been authenticated


| Entity Name | Create | Read | Update | Delete |
| :------: | :------: | :------: | :------: | :------: |
| Trading Post Listing Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Trading Post Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Admin Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Farm Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Milk Test Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Farmer Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Important Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Quality Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Technical Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Quality Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Technical Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Important Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| News Article Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Promoted Articles Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Agri Supply Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Sustainability Post Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Agri Supply Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |

---

### Admin Group  (Special) 
Users who have been created as an Admin entity

Users who belong to the admin group have access to the admin section of the application. This section allows them to control various aspects of the application such as entities and site settings.

| Entity Name | Create | Read | Update | Delete |
| :------: | :------: | :------: | :------: | :------: |
| Trading Post Listing Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Trading Post Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Admin Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Farm Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Milk Test Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Farmer Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Important Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Quality Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Technical Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Quality Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Technical Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Important Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| News Article Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Promoted Articles Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Agri Supply Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Sustainability Post Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Agri Supply Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |

---

### Farmer Group 
Users who have been created as a Farmer entity


| Entity Name | Create | Read | Update | Delete |
| :------: | :------: | :------: | :------: | :------: |
| Trading Post Listing Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Trading Post Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Admin Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Farm Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Milk Test Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Farmer Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Important Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Quality Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Technical Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Quality Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Technical Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Important Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| News Article Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Promoted Articles Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Agri Supply Document Category Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Sustainability Post Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |
| Agri Supply Document Entity | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> | <i class="fa fa-times"> |

---

