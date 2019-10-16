# Style

This document details different style choices that have been made throughout development. You may be directed to this during reviews for explainations on why you're being forced to style or design your code a certain way.

## Formatting

This section covers formatting that can't be controlled by outside tools like editorconfig.

#### static before readonly

It flows better and keeps field modifiers consistent.

#### Newline as a seperator

An empty newline should seperate APIs. This makes it easier to read and browse though.

## Design

#### Don't leak implementation details

If you provide an API that abstracts away some feature's implementation, don't leak implementation details about the abstraction whenever possible. Valve is a weird beast that doesn't care about us, so implementation details for an API can change at a moment's notice.

#### Be explicit

Implicit operators are discouraged, instead use methods, constructors, or explicit casts over implicit cast operators.