#!/bin/sh
cd /home/sc/dev/GoM/Tests/GoM.Feeds.Tests/
dotnet build
sleep 2
#dotnet test --no-build --filter "FullyQualifiedName=GoM.Feeds.Tests.NpmJsFactoryTests.sniff_js_with_list_of_uris"
#dotnet test --no-build --filter "FullyQualifiedName=GoM.Feeds.Tests.NpmJsFactoryTests.sniff_js_with_bad_single_uri_must_return_empty"
#dotnet test --no-build --filter "FullyQualifiedName=GoM.Feeds.Tests.NpmJsFactoryTests.sniff_js_with_proper_single_uri"

dotnet test --no-build --filter "FullyQualifiedName=GoM.Feeds.Tests.PypiFactoryTests.sniff_python_with_proper_single_uri"
dotnet test --no-build --filter "FullyQualifiedName=GoM.Feeds.Tests.PypiFactoryTests.sniff_python_with_bad_single_uri_must_return_empty"
dotnet test --no-build --filter "FullyQualifiedName=GoM.Feeds.Tests.PypiFactoryTests.sniff_python_with_list_of_uris"

