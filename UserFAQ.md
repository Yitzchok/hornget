# Introduction #

Add questions and answers here


# Using horn #

### How stable is horn? ###
  * Horn is not yet at the beta release stage. As such, there are a number of rough edges and the interface is not as easy to use as we would like. If you notice any problems, please have a look at the Issues list or ask on the mailing list.
  * Horn should be considered at **developers-only** stage. It has not been tested on a wide range of systems, and it is not yet ready for every day use, however, if you are prepared to put up with a development release, you may find it useful.

### Where can I download the source? ###

Source is available via the svn repository (see the source tab above). There are no packaged downloads available at this time.

### Where does Horn put the generated files? ###

If you don't specify an output path Horn puts the files:
  * ~/.horn/result
  * C:\Documents and Settings\user.name\.horn\result on WinXP
  * C:\Users\user.name\.horn\result on Vista

There is however also an option to specifiy where Horn should put the result files. You can use it as follows:
  * install:_component_ -output:_the output path of your choice_

# Creating package descriptions #

### How do I create a .boo file for my own library? ###
  * Have a look at the examples in your ~/.horn folder

### Building with MSBuild ###
  * You should set build\_root\_dir to something like "build" and that is also the directory that MSBuild will drop the output files.