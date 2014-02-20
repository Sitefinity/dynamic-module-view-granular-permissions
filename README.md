dynamic-module-view-granular-permissions
========================================

Add Read-Only permissions for dynamic content items

## Real sample

Your Sitefinity website supports a variety of marketing reports. Depending on the role you assign to the user, he or she can see specific types of reports. For example:

**Enterprise report** - only users with role "pm-enterprise" assigned can view this report

**Standard report** - users with role "pm-standard" assigned can view this report

**Everyone report** - everyone can view this report

**Everyone and Standard report** -users with role "pm-enterprise" and users with role "pm-standard" assigned can view this report

Administrators can see all reports no matter what permissions they have because their role is unrestricted.

In case you need to create a custom role that restricts dynamic items:
* Make sure the name of the role starts  with "pm-". This name tells the system the new role is specifically used to control the visibility of dynamic content items. 
* Create a permission taxon with the same name as the role. Thus, items that contain this taxon can be viewed only by users that have this specific role assigned, as the role name is the same as the name of the taxon.

If there is an item with a taxon that does not contain "pm-" in its name, the item is not displayed to other cuastom roles whose names start with "pm-". If you want to make a dynamic item visible to everyone, create a "pm-everyone" taxon and associate it to the specific item. 

In case user with specific role that starts with "pm-" tries to access item that he does not have permissions, a 403 error will be returned.

------

### Video example
 
[![video example](http://content.screencast.com/users/Veronica_Milcheva/folders/Default/media/3b2519ab-b235-4137-b1e4-fe5577cf212b/image.png)](http://www.screencast.com/t/2eTLJrMeG)


### Prerequisites

* Create a flat taxonomy of type "Permissions" that will relate the dynamic content item to the corresponding role. See
[Creating and editing a custom classification](http://www.sitefinity.com/documentation/documentationarticles/user-guide/website-content/classifying-your-content/creating-custom-classifications/creating-and-editing-a-custom-classification)
* Create several permissions taxons with the same names as the roles, for example "pm-enterprise" and "pm-standard". If you want to make the dynamic item visible to everyone, create a "pm-everyone" taxon. 
See
[Creating and editing single classification items](http://www.sitefinity.com/documentation/documentationarticles/user-guide/website-content/classifying-your-content/creating-custom-classifications/creating-and-editing-single-classification-items)
* Create 2 Sitefinity roles - "pm-enterprise" and "pm-standard".
See
[Creating and deleting roles](http://www.sitefinity.com/documentation/documentationarticles/installation-and-administration-guide/users-roles-and-permissions/managing-roles/creating-and-deleting-roles)
* Create 2 users and assign the roles from the previous step to the users.
See
[Creating and deleting users](http://www.sitefinity.com/documentation/documentationarticles/installation-and-administration-guide/users-roles-and-permissions/managing-users/creating-and-deleting-users)
* Create a module in the Module builder with classification field of type "Permissions"
See
[Creating the dynamic module](http://www.sitefinity.com/documentation/documentationarticles/creating-the-custom-module)
* Activate the module.
See
[Activating and deactivating a dynamic module   ](http://www.sitefinity.com/documentation/documentationarticles/activating-and-deactivating-a-module)
* Create several dynamic content items with taxons corresponding to the permissions the items have.
See
[Creating a dynamic content item](http://www.sitefinity.com/documentation/documentationarticles/user-guide/website-content/creating-and-using-custom-modules/using-your-custom-module/creating-a-custom-content-item)
* Create a Sitefinity page.
See
[Creating a new page](http://www.sitefinity.com/documentation/documentationarticles/user-guide/pages/creating-a-new-page)
* Drag and drop the dynamic widget to the page.
See
[Adding widgets on your page](http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets/adding-widgets-on-your-page)
* Publish the page

### Installation instructions

1. Add the code to the solution where your Sitefinity web application project is located.

2. Open the ToolboxesConfig.config file from *~/App_Data/Sitefinity/Configuration*.

3. Find your custom module in *ContentToolboxSection* tag and within the tag replace the *type* attribute with **SitefinityWebApp.DynamicContentViewCustom**. See the example below:

```xml
<toolboxesConfig xmlns:config="urn:telerik:sitefinity:configuration"
 xmlns:type="urn:telerik:sitefinity:configuration:type" config:version="6.X.XXXX.0">
	<toolboxes>
		<toolbox name="PageControls">
			<sections>
				<add name="ContentToolboxSection">
					<tools>
...
<add enabled="True" type="SitefinityWebApp.DynamicContentViewCustom,
 Telerik.Sitefinity" title="reports" cssClass="sfNewsViewIcn"
 moduleName="Marketing Reports" 
DynamicContentTypeName="Telerik.Sitefinity.DynamicTypes.Model.MarketingReports.Report" 
DefaultMasterTemplateKey="ef053cf3-9cb3-6844-bebb-ff00005ebe0b" 
DefaultDetailTemplateKey="f0053cf3-9cb3-6844-bebb-ff00005ebe0b" visibilityMode="None" 
name="Telerik.Sitefinity.DynamicTypes.Model.MarketingReports.Report
/>

```





*Build the solution.*

*Happy coding!*

### Requirements

* .NET Framework 4/4.5
* Visual Studio 2010/2012/2013
* Sitefinity 6.0+
* SQL Server 2008/2012

### History

2014-02-20 : Initial creation of the sample.

### Contact

If you have a bug report, a feedback, a suggestion, or just want to say hi, you can write us to the [Sitefinity Google+ group](https://plus.google.com/communities/101682685148530961591).
