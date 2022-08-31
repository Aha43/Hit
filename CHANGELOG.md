
# Changelog

## 8.0.2-alpha

* Depends Upgrades

## 8.0.0-alpha

### New

* Moved spcification of which appsetting json file to use from code to `SysCon` attribute (`JsonPath` parameter)
* Moved specification of if to read user secret from code to `SysCon` attribute (`UserSecrets` parameter)

## 7.0.0-alpha

### New

* Major breaking changes related to focus on the one, two or three dimensional aspect 'unit test space'
  * Configuration classes can now have multiple `SysCon` attributes and the attribute have new optional parameter `Layer` adding the *third dimension* (the first being *unit tests* and the second being *systems*).
  * Types `IUnitTestsSet` and `UnitTestsSet` to `IUnitTestsSpace` and `UnitTestsSpace`.
  * Configuration classes can now pick sections from a common appsetting using new `SysCon` attribute parameter `ConfigurationSections`. 
  * Utility to format log message
  * `IUnitTestsSpace.Dimension` property to tell dimension of the *unit tests space*
  * `IUnitTestsSpace.UnitTestCoordinates` method to get all unit tests coordinate tupples `(system, layer, unit-test)` 
  * Above is most important but not only changes and additions in order for API to be true to the dimensional aspect of *unit tests space*
  * *A lot of unit tests implemented, hopefully this mean api is getting stable 😀*

### DevOps

* Using ps1 script to push commits between releases (`push.ps1`)

## 6.2.1-alpha

### New

* Property on `IUnitTests` to get names of all unit tests
* Property on `IUnitTestsSet` to get names of all sets.

### DevOps

* Using ps1 script to push release (`publish_release.ps1`)

## 6.2.0-alpha

### New

* `ISystemConfiguration` implementations that extends `DefaultSystemConfigurationAdapter` can now pick sections from appsettings that is required for the configuration by invoking `GetPartConfiguration`.

## 6.1.0-alpha

### New

* Possible to say a system is not available for testing: New method in `ISystemConfiguration` : `Task<bool> AvailableAsync()`. Integration tests will not run and unit tests will not fail for system configured if returns false.

### Fix

* Fix so can read user secret for project using HIT when using DefaultSystemConfigurationAdapter

## 6.0.0-alpha

### Changes

* Interface that defines a Configuration with option that give name, optional description and optional test system (before given in option given when creating HitSuite)
* Got away with suite names, now we have IUnitTests (used to be IHitSuite) and IUnitTestsSet (IHitSuites)
* No longer support running unit tests in any way other than with a unit test framework 
* Make it mandatory for IUnitTests (used to be IHitSuite) to have name. Needed because now a IUnitTests is always indexed by name in a IUnitTestsSet

## 5.0.0-alpha

### Changes

* Lot of breaking changes relating to removal of concept 'test run', now called 'unit test'

## 4.0.0-alpha

### Changes

* Lot of breaking changes relating to only support running of a named run in a named suite.

## 3.1.0-alpha

### New

* Made TestRun UseAs attribute value "!" mean use test name as run test name

## 3.0.1-alpha

### Fix

* Fix that dependency injection did not work for IWorldProvider and ITestRunEventHandler implementations
* Fix that TestRun property not sat in TestContext passed to test and event handler

## 3.0.0-alpha

### Change

* IHitType and ITestRunEventHandler changed namespace

### New

* Adapter class for ITestRunEventHandler (TestRunEventHandlerAdapter) so need only implement wanted callbacks
* In an IHitSuites suites must have unique name
* IHitSuites.GetNamedSuite throws exception if suite not found, better for automatic testing

## 2.0.0-alpha

### Change

* Changed name on interface and base class implementing test logic (replaced ...Impl... with ...Logic...)
* Changed signature for methods implementing test logic (now takes a single argument of type ITestContext)

### New
* Interface for implmenting event handler for test run started, failed and ended events
* EnvionmentType in suite option, as suite property and test context property

## 1.0.2-alpha

First version dogfooded at day time work, worked :), produced some change requests...
