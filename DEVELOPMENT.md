# Development guide

## Git

### LFS

Ensure you have [Git LFS](https://git-lfs.github.com/) installed to help version large files, which is common in game development.

Try running `$ git lfs status`. If you get an error, run `$ git lfs install` to setup Git LFS for your user account.

### Unity

An optional but a reccommended step would be to setup the [Git for Unity](https://github.com/spoiledcat/git-for-unity) plugin.
The plugin is a Git client for the Unity editor, that also supports locking files (super useful when collaborating on Unity scenes)

To get setup, follow this [quick install guide](https://github.com/spoiledcat/git-for-unity#quick-install)

#### References

- [Using Git with Unity](https://gist.github.com/j-mai/4389f587a079cb9f9f07602e4444a6ed)
- [Git for Unity usage guide](https://github.com/spoiledcat/git-for-unity/blob/e0a52aba837d78baa4d6679e568a218591362eae/docs/using/quick-guide.md)

## Code style

We follow the [Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
to both demonstrate best C# practices and ensure a consistent look to the code.

## Commits

It is important to follow a few commit message conventions to ensure a uniform
commit history. We follow the [Conventional Commits specification](https://www.conventionalcommits.org/en/v1.0.0/).

Since we squash our commits from feature branches (see [Branching strategy](#branching-strategy)),
you can follow the commit style conventions for the PR title only. This means
that your individual commits on a feature branch do **not** need to follow the
commit conventions as long as your PR title does.

### Examples

- `feat: create scenes for game`
- `docs: correct spelling in README.md`
- `fix: prevent racing of requests`

## Issues

- GitHub issues will be used to track a bug or feature
    - A GitHub issue template is available for both
- Each issue can represent one or more feature branches
- Assign issues to yourself when working on it to minimise redundancy

## Sprints

- Our sprints are 2 weeks long
- GitHub's milestone feature will be used to track sprints
- Uncompleted issues for a given sprint will be moved to the following sprint
- At the end of every sprint, a tagged release is made given that the milestone
  is completed

## Branches

### Policies

The `main` branch has a few policies setup through GitHub
- Cannot be pushed to directly
- Requires a pull request before merging
- Pull requests require at least 1 approval before merging

### Branching strategy

> [Microsoft Git branching strategy](https://learn.microsoft.com/en-us/azure/devops/repos/git/git-branching-guidance)

![](https://user-images.githubusercontent.com/59267627/199492032-00ffa95f-4958-40bb-a10c-01a7e5ba8171.png)

- Use feature branches derived from the latest copy of `main` for all new features/bugfixes
- Merge feature branches into the `main` during sprints
- Keep a high quality, up-to-date main branch
- Commits on a feature branch will be squashed during merge

#### Naming

Use a consistent naming convention for your feature branches. This will help
identify the work done in the branch. Our branch naming conventions is similar to our
[commit conventions](#commits).

- `<type>/<issue_id>/<description>`
  - Examples include:
      - `feat/#1`
      - `feat/#1/add-scene`
      - `feat/add-development-doc`
      - `fix/#2`
      - `fix/#2/runtime-error`
  - Types include:
      - `feat`
      - `fix`
  - The issue ID is optional
  - Use issue ID when:
      - relevant issue is open
  - The description is optional
  - Use description when:
      - multiple branches for a single issue
      - issue description is abstract

### Pull requests

- Open a pull request (PR) for your feature branches
- Use the PR template to add as much information as you can about the
  feature/bugfix that isn't covered by the issue.
- Request a review from other code owners when branch is ready for review

#### Pipeline configuration

We have a development pipeline setup to check for formatting issues and to
ensure that project can be built without any failures. If the pipeline fails,
you can check the pipeline logs and use that to fix the issue. If the pipeline
failure is unrelated to the specified checks, please notify the Git admin.

The CI/CD pipeline is configured to run automatically after every commit.
For example, if documentation was updated, the pipeline will not run and will
continue to pass/fail depending on the previous result. However, if the code was
updated the pipeline will run to ensure that the project can be rebuilt.
