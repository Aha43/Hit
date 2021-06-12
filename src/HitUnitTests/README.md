A few unit tests is testing reading configuration data from user secrets. Since these can not be run at GitHub using actions they are only run if a file named `HitDev.txt`
that is not under source controll is present in unit tests root directory. To run all unit tests in a development environment do:

* Create a file named `HitDev.txt` in the unit tests root directory.
* Add as the unit test project user secrets the content of this [file](https://github.com/Aha43/Hit/blob/main/src/HitUnitTests/secret.json)
