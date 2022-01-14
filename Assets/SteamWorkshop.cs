using UnityEngine;
using Steamworks;
using System.Collections.Generic;
using UnityEngine.UI;
internal struct SteamWorkshopItem
{

    public string ContentFolderPath;
    public string Description;
    public string PreviewImagePath;
    public string[] Tags;
    public string Title;
}

public class SteamWorkshop : MonoBehaviour
{
    private SteamWorkshopItem currentSteamWorkshopItem;
    private PublishedFileId_t publishedFileID;
    public static SteamWorkshop Instance { get; private set; }

    public List<string> GetListOfSubscribedItemsPaths()
    {
        var subscribedCount = SteamUGC.GetNumSubscribedItems();
        PublishedFileId_t[] subscribedFiles = new PublishedFileId_t[subscribedCount];
        SteamUGC.GetSubscribedItems(subscribedFiles, (uint)subscribedFiles.Length);

        ulong sizeOnDisk = 0;
        string installLocation = string.Empty;
        uint timeStamp = 0;

        var result = new List<string>();

        foreach (var file in subscribedFiles)
        {
            SteamUGC.GetItemInstallInfo(file, out sizeOnDisk, out installLocation, 1024, out timeStamp);
            result.Add(installLocation);
        }

        return result;
    }
    
    public void UploadContent(string itemTitle, string itemDescription, string contentFolderPath, string[] tags, string previewImagePath)
    {
        Debug.Log("STARTING UPLOAD");
        currentSteamWorkshopItem = new SteamWorkshopItem
        {
            Title = itemTitle,
            Description = itemDescription,
            ContentFolderPath = contentFolderPath,
            Tags = tags,
            PreviewImagePath = previewImagePath
        };
        Debug.Log("CREATING ITEM");
        CreateItem();
    }


    private void CreateItem()
    {
        Debug.Log("CREATING ITEM 2");
        var steamAPICall = SteamUGC.CreateItem(SteamUtils.GetAppID(), EWorkshopFileType.k_EWorkshopFileTypeCommunity);
        Debug.Log("STEP 1");
        var steamAPICallResult = CallResult<CreateItemResult_t>.Create();
        Debug.Log("STEP 2");
        steamAPICallResult.Set(steamAPICall, CreateItemResult);
        Debug.Log("STEP 3");

    }

    private void CreateItemResult(CreateItemResult_t param, bool bIOFailure)
    {
        Debug.Log("ITEM RESULT");
        if (param.m_eResult == EResult.k_EResultOK)
        {
            publishedFileID = param.m_nPublishedFileId;
            Debug.Log("UPLOADING");
            UpdateItem();
        }
        else
        {
            Debug.Log("Couldn't create a new item");
        }
    }

    private void UpdateItem()
    {
        var updateHandle = SteamUGC.StartItemUpdate(SteamUtils.GetAppID(), publishedFileID);
        Debug.Log("UPDATING");
        SteamUGC.SetItemTitle(updateHandle, currentSteamWorkshopItem.Title);
        SteamUGC.SetItemDescription(updateHandle, currentSteamWorkshopItem.Description);
        SteamUGC.SetItemContent(updateHandle, currentSteamWorkshopItem.ContentFolderPath);
        SteamUGC.SetItemTags(updateHandle, currentSteamWorkshopItem.Tags);
        SteamUGC.SetItemPreview(updateHandle, currentSteamWorkshopItem.PreviewImagePath);
        SteamUGC.SetItemVisibility(updateHandle, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic);
        Debug.Log("CALLING API");
        var steamAPICall = SteamUGC.SubmitItemUpdate(updateHandle, "");
        var steamAPICallResult = CallResult<SubmitItemUpdateResult_t>.Create();
        steamAPICallResult.Set(steamAPICall, UpdateItemResult);
    }

    private void UpdateItemResult(SubmitItemUpdateResult_t param, bool bIOFailure)
    {
        Debug.Log("TAKING RESULT");
        if (param.m_eResult == EResult.k_EResultOK)
        {
            Debug.Log("Sucessfully submitted item to Steam");
        }
        else
        {
            Debug.Log("Couldn't submit the item to Steam");
        }
    }
}