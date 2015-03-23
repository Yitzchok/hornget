The sample below describes the various options within the Horn DSL:

```
install <PackageID>:
    get_from git | hg | svn (<URI>)
    build_with nant | msbuild | rake | phantom, buildfile(<PATH>), FrameworkVersion2 | FrameworkVersion35

    switches:
        parameters <VALUE>, <VALUE>

    generate_strong_key

    with:
        tasks <TASK>, <TASK>

    build_root_dir <PATH>
    shared_library <PATH>

dependencies:
    depend <PACKAGE> >>  <VERSION> >>  <ASSEMBLYNAME>

package.category = <VALUE>
package.description = <VALUE>
package.forum = <VALUE>
package.homepage = <VALUE>
package.name = <VALUE>
package.notes = <VALUE>
package.version = <VALUE>
```