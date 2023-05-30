using PlayFab;
using UnityEngine;
using PlayFab.GroupsModels;
using PlayFab.ClientModels;
using System;
using EntityKey = PlayFab.GroupsModels.EntityKey;

public class MyPlayfabTestArea : MonoBehaviour
{
    string entityId;
    string entityType;
    const string eID = "21BBC7067F01F296";
    string eTYPE = "title_player_account";
    string groupID = "570950BBD04D01CB";
    const string playerID = "1C236D510BF5DCDB";
    private void Start()
    {
        loginPlayer(599.ToString());
    }
    public void loginPlayer(string email)
    {
        var request = new LoginWithEmailAddressRequest()
        {
            Email = "\"" + email + "\"" + "@gmail.com",
            Password = "______"
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, onloginsuccess, onerror);

    }
    void onloginsuccess(LoginResult result)
    {
        Debug.Log("login success");
        getGroup();
    }


    #region createGroup

    public void createGroup()
    {
        var request = new CreateGroupRequest()
        {
            GroupName = "MyGroupTest"
        };

        PlayFabGroupsAPI.CreateGroup(request, createGroupSuccess, onerror);

    }
    void createGroupSuccess(CreateGroupResponse response)
    {
        Debug.Log(response);
    }

    #endregion

    #region acceptGroupInvitation

    void acceptGroupInv()
    {
        var request = new AcceptGroupInvitationRequest()
        {
            Group = entityId
        };
        PlayFabGroupsAPI.AcceptGroupInvitation(request, onsuccess, onerror);
    }

    void onsuccess(PlayFab.GroupsModels.EmptyResponse obj)
    {
        Debug.Log(obj);
    }


    #endregion

    #region addMember

    public void addMember(string s )
    {
        var request = new AddMembersRequest()
        {
            Group = s
            //Members ={ "1C236D510BF5DCDB" }
        };
        PlayFabGroupsAPI.AddMembers(request, addMemberSuccess, onerror);
    }

    private void addMemberSuccess(PlayFab.GroupsModels.EmptyResponse obj)
    {
        Debug.Log("Members added successfully!");
    }

    #endregion

    #region inviteToGroup

    void inviteToGroup(string id)
    {
        var request = new InviteToGroupRequest()
        {
            Group = id
        };

        PlayFabGroupsAPI.InviteToGroup(request, onsucc, onerror);
        Debug.Log("inviting");
    }

    private void onsucc(InviteToGroupResponse response)
    {
        Debug.Log(response.Group);
    }

    #endregion

    #region getGroup

    void getGroup()
    {
        var request = new GetGroupRequest()
        {
            GroupName = "TestGroup"
        };
        PlayFabGroupsAPI.GetGroup(request, ons, onerror);
    }
    void ons(GetGroupResponse response)
    {
        Debug.Log(response.GroupName);
        Debug.Log(response.Group.Id);
        entityId = response.Group.Id;
        entityType = response.Group.Type;
        //listGroupMembers(response.Group.ToString());
        //listMembership();
        //listGroup();
        //inviteToGroup(entityId);
    }

    #endregion

    #region listGroupMembers

    void listGroupMembers(string s)
    {
        var request = new ListGroupMembersRequest()
        {
            Group = s
        };
        PlayFabGroupsAPI.ListGroupMembers(request, onsu, onerror);
    }

    private void onsu(ListGroupMembersResponse obj)
    {
        Debug.Log(obj.Members);
        throw new NotImplementedException();
    }

    #endregion

    #region listMembership

    void listMembership()
    {
        var request = new ListMembershipRequest()
        {
            Entity = new EntityKey()
            {
                Id = eID,
                Type = eTYPE
            }
        };
        PlayFabGroupsAPI.ListMembership(request, onlistsuccess, onerror);
    }
    void onlistsuccess(ListMembershipResponse response)
    {
        Debug.Log(response.Groups[0].GroupName);
        Debug.Log(response.Groups[0].Group.Id);
        string s = response.Groups[0].Group.Id;
        //addMember(s);
        //listGroupMembers(s);
        //string s = response.Groups[0].Group;
        //listGroupMembers(s);
    }

    #endregion

    void onerror(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}

