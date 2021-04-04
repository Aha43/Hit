# HIT - Hierarchically Integration Test framework

A framework for integration tests where the work of one test can be the build up for the next tests.

### Example of testing CRUD operations as defined by the interface [IItemRepository](https://github.com/Aha43/Hit/blob/main/sample_system_src/Items.Specification/IItemsRepository.cs) using HIT

HIT tests have an implementation class decorated with the `UseAs` attribute that tells how it is used to realize tests in a suite. Here is a test implementation that test creating an item given a repository for items:
