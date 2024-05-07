#!/bin/sh

set -eu # Add -xv flags for debugging

## Uncomment if you want to check for root permissions
# [ $UID -eq 0 ] || (echo "ERROR: User must be root" 1>&2 && exit 1)

RED='\033[0;31m'
ORANGE='\033[0;33m]'
GREEN='\033[0;32m]'
NC='\033[0m' # No Color

which dotnet-format > /dev/null 2>&1 || (printf "${RED}ERROR${NC}: You need to have installed dotnet-format nuget package globally.\n" && exit 1)
git config core.hookspath .githooks/
