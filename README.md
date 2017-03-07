# Sitcom.exe
Tool to track some info about tv shows. 

Windows version of [sitcom](https://github.com/edghto/sitcom).

## Description
  ```
  add        Add new season to sitcom
  delete     Delete sitcom or season of sitcom
  list       List stored sitcoms with all seasons
  last       Set last watched episode of sitcom
  report     Report requested sitcoms
  help       Display more information on a specific command.
  version    Display version information.
  ```
  
### Add
  ```
  -f, --file      Required. Source file name
  -s, --season    Season number (override season extracted from file)
  value pos. 0    Required. Sitcom name
  ```
  
  ```
  Add new season for sitcom foo (extract season number from file)
  > Sitcom.exe add -f E:\Download\foo.htm foo
  
  Add new season for sitcom foo (override season number extractred from file)
  > Sitcom.exe add -f E:\Download\foo.htm -s 3 foo
  ```
  
### List
   ```
   > Sitcom.exe list
   ```
   
### Delete
  ```
  -s, --season    Season number
  value pos. 0    Required. Sitcom name
  ```
  
  ```
  Delete season 3 of sitcom foo
  > Sitcom.exe -s 3 foo  
  
  Delete all seasons of sitcom foo
  > Sitcom.exe foo
  ```

### Last
  ```
  -s, --season    Required. Season number
  value pos. 0    Required. Sitcom name
  value pos. 1    Required. Episode number
  ```

  ```
  Set episode 13 as last watched for season 5 of sitcom foo
  > Sitcom.exe -s 5 foo 13
  ```
  
### Report
  ```
  value pos. 0    Names of requested sitcoms, accepted formats name[:season]
  ```
  
  ```
  List last season of sitcom foo
  > Sitcom.exe report foo
  
  List 3rd season of sitcom foo
  > Sitcom.exe report foo:3
  
  List 3rd and 7th season of sitcom foo
  > Sitcom.exe report foo:3 foo:7
  ```

## Dependencies
  * `EntityFramework 6.0` peristance storage framwork
  * `CommandLineParser 2.0.275.0` parsing command line application
  * `HtmlAgilityPack 1.4.9.5` parsing html downloaded from IMDB
  * `MSTest 10.1.0.0` unit testing framework
  * `Moq 4.5.30.0` Mocking framework
  
