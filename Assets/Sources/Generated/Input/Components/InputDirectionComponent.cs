//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public DirectionComponent direction { get { return (DirectionComponent)GetComponent(InputComponentsLookup.Direction); } }
    public bool hasDirection { get { return HasComponent(InputComponentsLookup.Direction); } }

    public void AddDirection(float newX, float newY) {
        var index = InputComponentsLookup.Direction;
        var component = CreateComponent<DirectionComponent>(index);
        component.x = newX;
        component.y = newY;
        AddComponent(index, component);
    }

    public void ReplaceDirection(float newX, float newY) {
        var index = InputComponentsLookup.Direction;
        var component = CreateComponent<DirectionComponent>(index);
        component.x = newX;
        component.y = newY;
        ReplaceComponent(index, component);
    }

    public void RemoveDirection() {
        RemoveComponent(InputComponentsLookup.Direction);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.MatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherDirection;

    public static Entitas.IMatcher<InputEntity> Direction {
        get {
            if(_matcherDirection == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Direction);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherDirection = matcher;
            }

            return _matcherDirection;
        }
    }
}
