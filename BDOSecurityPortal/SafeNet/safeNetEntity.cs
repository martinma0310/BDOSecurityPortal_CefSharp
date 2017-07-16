using System;
using System.Collections.Generic;
using System.Text;
internal class safeNetEntity
{
    internal static string vendorCode =
              "GBuud4gmFfZTEJAHH59QJWTdDyui4fI7wUTSfS/xsQzy5uyMOefCWWzO42dn1R/GCOhFYLq+jXghP1C7" +
              "gWA7yRxghZ6j7/EIeYX0DFd0m0v5gGNIF86boL2SH1ILn8d4HNmn1YkTnDOPdMJwDNdd6ee/ZwwoqR2N" +
              "CVH5tM4DRw04YdJGS1Ju33ZDr79VlLvJ7MyKKZvlVYMr36ZxMFEQC4AW3qIW5BrBhucxUTDPQQwvDLeE" +
              "wbM6CcHHuO4J8yBsj1Y8xEs2LyED95gzCc2M/MJogcPXG0yLy/eCEo/ro6D5ry+4oAMe9m4gcsAil3Sx" +
              "EphYRpijUx9uTJeAo+q1P4IXmfElEW3DM3Ndyut1ZOCUZJqX1Z3h28qzVQ2R7+v7HbX80dUr6/Z80JPv" +
              "ApW0gFU+lVia3sBSNHl/T2DfmPViFoCKWRjldlGk1WkVYsni7YVMP5yUMWH9B8T9R/DFk66RzNg5KB0z" +
              "VhAvjsfzuuiIJlD8fje/H4TuiLlpIaf8QfSpqzYXIcmvFWj0NKog/5XDBvpGdp06eFgWdF+MB9zJQZPY" +
              "olL0o1UITPpkwwJe7aIeJxtCKrsdXmQJ9g4Fkf6a7K70r5RwMXIKIplIy+F/qwyuYimNsUF8QSkjDBov" +
              "29HjerSOxR0Io7eOULMNE+Cmgq5//n6+s4dY+qiL8ZFw3vrYbpdC+xQEzVJp/19pfjzFxVkxxyeGjvdl" +
              "HlU8eXakS4rjJyOWyvteSmK5mK1HkFqKgSRVk+DL/RHvSZgg6jojhwv/1iwBZas20IaEAZ3Qzxnf6eAg" +
              "CFq9uzxWdGgmQzDBE+srSO1xW9oWK9ZkHgYxgDBgQvYhJAtIXwiMQNd+cuHL8/2uKCowpXZl1RXgiUVF" +
              "GiLoBHRxn1Efmf88pOQQOeOFZu8OQ3Nyb6inWvye+/Rld5u2KUCZ++DmP50=";


            //"AzIceaqfA1hX5wS+M8cGnYh5ceevUnOZIzJBbXFD6dgf3tBkb9cvUF/Tkd/iKu2fsg9wAysYKw7RMAsV" +
            //"vIp4KcXle/v1RaXrLVnNBJ2H2DmrbUMOZbQUFXe698qmJsqNpLXRA367xpZ54i8kC5DTXwDhfxWTOZrB" +
            //"rh5sRKHcoVLumztIQjgWh37AzmSd1bLOfUGI0xjAL9zJWO3fRaeB0NS2KlmoKaVT5Y04zZEc06waU2r6" +
            //"AU2Dc4uipJqJmObqKM+tfNKAS0rZr5IudRiC7pUwnmtaHRe5fgSI8M7yvypvm+13Wm4Gwd4VnYiZvSxf" +
            //"8ImN3ZOG9wEzfyMIlH2+rKPUVHI+igsqla0Wd9m7ZUR9vFotj1uYV0OzG7hX0+huN2E/IdgLDjbiapj1" +
            //"e2fKHrMmGFaIvI6xzzJIQJF9GiRZ7+0jNFLKSyzX/K3JAyFrIPObfwM+y+zAgE1sWcZ1YnuBhICyRHBh" +
            //"aJDKIZL8MywrEfB2yF+R3k9wFG1oN48gSLyfrfEKuB/qgNp+BeTruWUk0AwRE9XVMUuRbjpxa4YA67SK" +
            //"unFEgFGgUfHBeHJTivvUl0u4Dki1UKAT973P+nXy2O0u239If/kRpNUVhMg8kpk7s8i6Arp7l/705/bL" +
            //"Cx4kN5hHHSXIqkiG9tHdeNV8VYo5+72hgaCx3/uVoVLmtvxbOIvo120uTJbuLVTvT8KtsOlb3DxwUrwL" +
            //"zaEMoAQAFk6Q9bNipHxfkRQER4kR7IYTMzSoW5mxh3H9O8Ge5BqVeYMEW36q9wnOYfxOLNw6yQMf8f9s" +
            //"JN4KhZty02xm707S7VEfJJ1KNq7b5pP/3RjE0IKtB2gE6vAPRvRLzEohu0m7q1aUp8wAvSiqjZy7FLaT" +
            //"tLEApXYvLvz6PEJdj4TegCZugj7c8bIOEqLXmloZ6EgVnjQ7/ttys7VFITB3mazzFiyQuKf4J6+b/a/Y";

    internal static string loginScope =
        "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
        "<haspscope>" +
            "<hasp id=\"{slcode}\" />" +
        "</haspscope>";

    internal static string scope =
            "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
            "<haspscope>" +
            "    <license_manager hostname=\"localhost\" />" +
            "</haspscope>";

    //internal static string scopeLogin =
    //   "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
    //   "<haspscope/>";


    internal static string format =
            "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
            "<haspformat root=\"hasp_info\">" +
            "    <hasp>" +
            "        <attribute name=\"id\" />" +
            "        <attribute name=\"type\" />" +
            "        <feature>" +
            "            <attribute name=\"id\" />" +
            "        </feature>" +
            "    </hasp>" +
            "</haspformat>";

    internal static string formatInfo = 
        "<haspformat root=\"hasp_info\">" + 
        "    <feature>" + 
        "       <attribute name=\"id\" />" + 
        "       <attribute name=\"locked\" />" + 
        "       <attribute name=\"expired\" />" + 
        "       <attribute name=\"disabled\" />" + 
        "       <attribute name=\"usable\" />" + 
        "    </feature>" + 
        "</haspformat>";
    internal static string formatC2V =
        "<haspformat format=\"host_fingerprint\"/>";

    internal static string formatImmigration =
        "<haspformat root=\"location\">" +
        "    <license_manager>" +
        "        <attribute name=\"id\" />" +
        "        <attribute name=\"time\" />" +
        "        <element name=\"hostname\" />" +
        "        <element name=\"version\" />" +
        "        <element name=\"host_fingerprint\" />" +
        "     </license_manager>" +
        "</haspformat>";

    internal static string actionRehost =
        "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
        "<rehost><hasp id=\"{slcode}\" /></rehost>";

    internal static string scopeRehost =
    "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
    "<haspscope>" +
    "    <hasp id=\"{slcode}\" />" +
    "</haspscope>" +
    "";

    internal static string address = "http://sso.pcpa.cn/HASPClient";

    internal static uint port = 0;

    internal static string version = "application/vnd.ems.v12";
    internal static string url = "http://sso.pcpa.cn/";
    internal static Encoding encoding = Encoding.UTF8;
}
