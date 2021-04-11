
# Changelog

<!--
## Unreleased
-->
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
