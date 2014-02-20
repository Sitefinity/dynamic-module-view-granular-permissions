dynamic-module-view-granular-permissions
========================================

Add Read-Only permissions for dynamic content items

## Real sample

Your Sitefinity website supports different marketing reports. According to the role of the user, he/she must see different reports. For example:

**Enterprise report** - will be viewed only by users of role "pm-enterprise". 

**Standard report** - will be viewed by users of role "pm-standard".

**Everyone report** - will be biewed by everyone.

**Everyone and Standard report** - will be viewed for both users with roles "pm-enterprise" and users with role "pm-standard". 

Administrators will see all reports no matter what permissions they have as their role is unrestricted.

If you want to create another special role that will restrict dynamic items, make sure to create the name, starting  with "pm-". This tells the system that this role is specifically used to control the visibility of dynamic content items. Also create a permission taxon with the same name as the role again starting with "pm-". From now on items that contain this taxon will be viewed only by the role with name that equals to the name of the taxon.

If there's an item with any taxon that does not contains "pm" in the name, the item will not be displayed for other special roles that start with "pm", except standard Sitefinity roles. If you want to make the dynamic item visible for everyone - put "pm-everyone" taxon.

------

### Video example
 
[![video example](http://content.screencast.com/users/Veronica_Milcheva/folders/Default/media/3b2519ab-b235-4137-b1e4-fe5577cf212b/image.png)](http://www.screencast.com/t/2eTLJrMeG)


### Prerequisites

* Create a Flat Taxonomy of type "Permissions" that will relate the dynamic content item with the corresponding role. 
* Create several permissions taxons with the same names as the roles , e.g. "pm-enterprise", "pm-standard". If you want to make the dynamic item visible for everyone - you need to create a "pm-everyone" taxon. 
* Create 2 Sitefinity roles - "pm-enterprise" and "pm-standard".
* Create 2 users with the roles from the previous step.
* Create Module builder module with Classification field of type "Permissions"
* Activate the module.
* Create several dynamic content items with taxons according to the permissions they will have.
* Create Sitefinity Page.
* Drag the dynamic widget to the page.
* Publish the page. 

### Installation instructions

Installation instructions:

1. Add the code to a solution in which you have Sitefinity Web Application project.

2. Open ToolboxesConfig.config from ~/App_Data/Sitefinity/Configuration and open it for edit.

3. Find your custom module in ContentToolboxSection and replace the type with **SitefinityWebApp.DynamicContentViewCustom**. See the example below:

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





*Build the solution and you should be good to go.*

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
