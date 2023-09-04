package com.picoxr.tobservice;

import android.annotation.SuppressLint;
import android.annotation.TargetApi;
import android.content.Context;
import android.os.Build;
import android.os.CpuUsageInfo;
import android.os.RemoteException;
import android.util.Log;

import com.picoxr.tobservice.interfaces.BoolCallback;
import com.picoxr.tobservice.interfaces.IntCallback;
import com.picoxr.tobservice.interfaces.StringCallback;
import com.pvr.tobservice.ToBServiceHelper;
import com.pvr.tobservice.enums.PBS_ControllerPairTimeEnum;
import com.pvr.tobservice.enums.PBS_CustomizeSettingsTabEnum;
import com.pvr.tobservice.enums.PBS_DeviceControlEnum;
import com.pvr.tobservice.enums.PBS_HomeEventEnum;
import com.pvr.tobservice.enums.PBS_HomeFunctionEnum;
import com.pvr.tobservice.enums.PBS_PackageControlEnum;
import com.pvr.tobservice.enums.PBS_PowerOnOffLogoEnum;
import com.pvr.tobservice.enums.PBS_ScreenOffDelayTimeEnum;
import com.pvr.tobservice.enums.PBS_SystemFunctionSwitchEnum;
import com.pvr.tobservice.enums.PBS_WifiDisplayModel;
import com.pvr.tobservice.interfaces.IBoolCallback;
import com.pvr.tobservice.interfaces.IGetControllerPairTimeCallback;
import com.pvr.tobservice.interfaces.IIntCallback;
import com.pvr.tobservice.interfaces.IStringCallback;
import com.pvr.tobservice.interfaces.IToBService;
import com.pvr.tobservice.interfaces.IToBServiceProxy;
import com.pvr.tobservice.interfaces.IWDJsonCallback;
import com.pvr.tobservice.interfaces.IWDModelsCallback;
import com.pvr.tobservice.model.MarkerInfo;
import com.pvr.tobservice.model.PicoCastMediaFormat;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;
import java.util.function.Consumer;

public class ToBServiceUtils {
    private static final String TAG = "ToBServiceUtils";
    private static ToBServiceUtils instance;
    private BoolCallback mCallBack;
    private static boolean isbinded = false;
    private Context mycontext = null;
    private IToBServiceProxy serviceBinder = null;

    public static ToBServiceUtils getInstance() {
        if (instance == null) {
            instance = new ToBServiceUtils();
        }
        return instance;
    }

    private ToBServiceUtils() {
    }


    public void setBindCallBack(BoolCallback callBack) {
        this.mCallBack = callBack;
        ToBServiceHelper.getInstance().setBindCallBack(new ToBServiceHelper.BindCallBack() {
            @Override
            public void bindCallBack(Boolean aBoolean) {
                isbinded = aBoolean;
                mCallBack.CallBack(aBoolean);
            }
        });
    }

    public void bindTobService(Context context) {
        mycontext = context;
        ToBServiceHelper.getInstance().bindTobService(mycontext);
    }

    public void unBindTobService() {
        if (isbinded) {
            isbinded = false;
            ToBServiceHelper.getInstance().unBindTobService(mycontext);
            serviceBinder = null;
        } else {
            Log.i(TAG, "service is not binded");
        }
    }

    public IToBServiceProxy getServiceBinder() {
        serviceBinder = (IToBServiceProxy) ToBServiceHelper.getInstance().getServiceBinder();
        return serviceBinder;
    }

    public void pbsControlSetDeviceAction(PBS_DeviceControlEnum deviceControlEnum, IntCallback callback) {

        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsControlSetDeviceAction(deviceControlEnum, new IIntCallback.Stub() {
                        @Override
                        public void callback(int i) throws RemoteException {
                            callback.CallBack(i);
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsControlSetDeviceAction: not bind ToBService");
        }
    }

    public void pbsControlAPPManger(PBS_PackageControlEnum packageControlEnum, String path, int ext,
                                    IntCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsControlAPPManger(packageControlEnum, path, ext, new IIntCallback.Stub() {
                        @Override
                        public void callback(int i) throws RemoteException {
                            callback.CallBack(i);
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsControlAPPManger: not bind ToBService");
        }
    }

    public void pbsControlSetAutoConnectWIFI(String ssid, String pwd, int ext, BoolCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsControlSetAutoConnectWIFI(ssid, pwd, ext, new IBoolCallback.Stub() {
                        @Override
                        public void callBack(boolean result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsControlSetAutoConnectWIFI: not bind ToBService");
        }
    }

    public void pbsControlClearAutoConnectWIFI(BoolCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsControlClearAutoConnectWIFI(new IBoolCallback.Stub() {

                        @Override
                        public void callBack(boolean result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsControlClearAutoConnectWIFI: not bind ToBService");
        }
    }

    public void pbsPropertySetHomeKey(PBS_HomeEventEnum event, PBS_HomeFunctionEnum function, BoolCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsPropertySetHomeKey(event, function, new IBoolCallback.Stub() {

                        @Override
                        public void callBack(boolean result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsPropertySetHomeKey: not bind ToBService");
        }
    }

    public void pbsPropertySetHomeKeyAll(PBS_HomeEventEnum event, PBS_HomeFunctionEnum function, int timesetup,
                                         String pkg, String className, BoolCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsPropertySetHomeKeyAll(event, function, timesetup, pkg, className, new IBoolCallback.Stub() {
                        @Override
                        public void callBack(boolean result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    });

                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsPropertySetHomeKeyAll: not bind ToBService");
        }
    }

    public void pbsPropertyDisablePowerKey(boolean isSingleTap, boolean enable, IntCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsPropertyDisablePowerKey(isSingleTap, enable, new IIntCallback.Stub() {
                        @Override
                        public void callback(int result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsPropertyDisablePowerKey: not bind ToBService");
        }
    }

    public void pbsPropertySetScreenOffDelay(PBS_ScreenOffDelayTimeEnum timeEnum, IntCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsPropertySetScreenOffDelay(timeEnum, new IIntCallback.Stub() {

                        @Override
                        public void callback(int result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    });

                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsPropertySetScreenOffDelay: not bind ToBService");
        }
    }

    public void pbsSetControllerPairTime(PBS_ControllerPairTimeEnum timeEnum,
                                         IntCallback callback, int ext) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsSetControllerPairTime(timeEnum, new IIntCallback.Stub() {

                        @Override
                        public void callback(int result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    }, ext);
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsSetControllerPairTime: not bind ToBService");
        }
    }

    public void pbsGetControllerPairTime(IntCallback callback, int ext) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsGetControllerPairTime(new IGetControllerPairTimeCallback.Stub() {

                        @Override
                        public void callBack(PBS_ControllerPairTimeEnum result)
                                throws RemoteException {
                            int mresult = result.ordinal();
                            callback.CallBack(mresult);
                        }
                    }, ext);
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsGetControllerPairTime: not bind ToBService");
        }
    }

    public void pbsWriteConfigFileToDataLocal(String path, String content, BoolCallback boolCallback) {
        if (serviceBinder != null) {
            try {
                if (boolCallback != null) {
                    serviceBinder.pbsWriteConfigFileToDataLocal(path, content, new IBoolCallback.Stub() {
                        @Override
                        public void callBack(boolean result) throws RemoteException {
                            boolCallback.CallBack(result);
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsWriteConfigFileToDataLocal: not bind ToBService");
        }
    }

    public void pbsResetAllKeyToDefault(BoolCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsResetAllKeyToDefault(new IBoolCallback.Stub() {
                        @Override
                        public void callBack(boolean result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsResetAllKeyToDefault: not bind ToBService");
        }
    }

    public void pbsSetWDModelsCallback(StringCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsSetWDModelsCallback(new IWDModelsCallback.Stub() {
                        @Override
                        public void callback(List<PBS_WifiDisplayModel> models) throws RemoteException {
                            try {
                                JSONArray result = new JSONArray();
                                for (PBS_WifiDisplayModel member : models) {
                                    result.put(new JSONObject(member.toJson()));
                                }
                                callback.CallBack(result.toString());
                            } catch (JSONException e) {
                                throw new RuntimeException(e);
                            }
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsSetWDModelsCallback: not bind ToBService");
        }
    }

    public void pbsSetWDJsonCallback(StringCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsSetWDJsonCallback(new IWDJsonCallback.Stub() {
                        @Override
                        public void callback(String json) throws RemoteException {
                            callback.CallBack(json);
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsSetWDJsonCallback: not bind ToBService");
        }
    }

    public void pbsSwitchLargeSpaceScene(BoolCallback callback, boolean open, int ext) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsSwitchLargeSpaceScene(new IBoolCallback.Stub() {
                        @Override
                        public void callBack(boolean result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    }, open, ext);
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsSwitchLargeSpaceScene: not bind ToBService");
        }
    }

    public void pbsGetSwitchLargeSpaceStatus(StringCallback callback, int ext) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsGetSwitchLargeSpaceStatus(new IStringCallback.Stub() {

                        @Override
                        public void callback(String value) throws RemoteException {
                            callback.CallBack(value);
                        }
                    }, ext);
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsGetSwitchLargeSpaceStatus: not bind ToBService");
        }
    }

    public void pbsExportMaps(BoolCallback callback, int ext) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsExportMaps(new IBoolCallback.Stub() {
                        @Override
                        public void callBack(boolean result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    }, ext);
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsExportMaps: not bind ToBService");
        }
    }

    public void pbsImportMaps(BoolCallback callback, int ext) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsImportMaps(new IBoolCallback.Stub() {
                        @Override
                        public void callBack(boolean result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    }, ext);
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsImportMaps: not bind ToBService");
        }
    }

    public void pbsControlSetAutoConnectWIFIWithErrorCodeCallback(String ssid, String pwd, int ext, IntCallback callback) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsControlSetAutoConnectWIFIWithErrorCodeCallback(ssid, pwd, ext, new IIntCallback.Stub() {

                        @Override
                        public void callback(int result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    });
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsPlayerGetCurPlayState: not bind ToBService");
        }
    }

    public int pbsIsVolumeChangeToHomeAndEnter() {
        int result = 0;
        if (serviceBinder != null) {
            try {
                result = serviceBinder.pbsIsVolumeChangeToHomeAndEnter().ordinal();
            } catch (RemoteException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsIsVolumeChangeToHomeAndEnter: not bind ToBService");
        }
        return result;
    }

    public int pbsControlGetPowerOffWithUSBCable(int ext) {
        int result = 0;
        if (serviceBinder != null) {
            try {
                result = serviceBinder.pbsControlGetPowerOffWithUSBCable(ext).ordinal();
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsControlGetPowerOffWithUSBCable: not bind ToBService");
        }
        return result;
    }

    public int pbsPropertyGetScreenOffDelay(int ext) {
        int result = 0;
        if (serviceBinder != null) {
            try {
                result = serviceBinder.pbsPropertyGetScreenOffDelay(ext).ordinal();
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsPropertyGetScreenOffDelay: not bind ToBService");
        }
        return result;
    }

    public int pbsPropertyGetSleepDelay(int ext) {
        int result = 0;
        if (serviceBinder != null) {
            try {
                result = serviceBinder.pbsPropertyGetSleepDelay(ext).ordinal();
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsPropertyGetSleepDelay: not bind ToBService");
        }
        return result;
    }

    public void pbsGetSwitchSystemFunctionStatus(
            PBS_SystemFunctionSwitchEnum systemFunction, IntCallback callback,
            int ext) {
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsGetSwitchSystemFunctionStatus(systemFunction, new IIntCallback.Stub() {

                        @Override
                        public void callback(int result) throws RemoteException {
                             callback.CallBack(result);
                        }
                    }, ext);
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsGetSwitchSystemFunctionStatus: not bind ToBService");
        }
    }

    public int pbsPicoCastInit(IntCallback callback, int ext) {

        int result = 0;
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    serviceBinder.pbsPicoCastInit(new IIntCallback.Stub() {

                        @Override
                        public void callback(int result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    }, ext);
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsPicoCastInit: not bind ToBService");
        }
        return result;
    }

    public int pbsGetScreenCastAudioOutput(int ext) {
        int result = 0;
        if (serviceBinder != null) {
            try {
                result = serviceBinder.pbsGetScreenCastAudioOutput(ext).ordinal();
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsGetScreenCastAudioOutput: not bind ToBService");
        }
        return result;
    }

    public int pbsGetCustomizeSettingsTabStatus(PBS_CustomizeSettingsTabEnum customizeSettingsTabEnum, int ext) {
        int result = 0;
        if (serviceBinder != null) {
            try {
                result = serviceBinder.pbsGetCustomizeSettingsTabStatus(customizeSettingsTabEnum, ext).ordinal();
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsGetCustomizeSettingsTabStatus: not bind ToBService");
        }
        return result;
    }

    public void pbsConnectWifiDisplay(String modelJson) {
        if (serviceBinder != null) {
            try {
                PBS_WifiDisplayModel model = new PBS_WifiDisplayModel();
                model = model.fromJson(modelJson);
                serviceBinder.pbsConnectWifiDisplay(model);
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsUnityConnectWifiDisplay: not bind ToBService");
        }
    }

    public String pbsGetConnectedWD() {
        String modelJson = null;
        if (serviceBinder != null) {
            try {
                PBS_WifiDisplayModel model = serviceBinder.pbsGetConnectedWD();
                if (model != null)
                    modelJson = model.toJson();
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsUnityGetConnectedWD: not bind ToBService");
        }
        return modelJson;
    }

    @SuppressLint("NewApi")
    public float[] pbsGetCpuUsages() {

        if (serviceBinder != null) {
            try {

                CpuUsageInfo[] cpus = serviceBinder.pbsGetCpuUsages();
                int size = cpus.length;
                float cpuus[] = new float[size];
                for (int i = 0; i < cpus.length; i++) {
                    cpuus[i] = (float) (cpus[i].getActive()) / cpus[i].getTotal();
                }
                return cpuus;
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsGetCpuUsages: not bind ToBService");
        }
        return null;
    }

    public int pbsSetSystemCountryCode(String countryCode, IntCallback callback, int ext) {
        int result = 0;
        if (serviceBinder != null) {
            try {
                if (callback != null) {
                    result = serviceBinder.pbsSetSystemCountryCode(countryCode, new IIntCallback.Stub() {

                        @Override
                        public void callback(int result) throws RemoteException {
                            callback.CallBack(result);
                        }
                    }, ext);
                }
            } catch (RemoteException e) {
                e.printStackTrace();
            }
        } else {
            Log.e(TAG, "pbsSetSystemCountryCode: not bind ToBService");
        }
        return result;
    }
    @TargetApi(Build.VERSION_CODES.N)
    public void pbsSetIPD(float var1, IntCallback callback) {
        if (serviceBinder != null) {
            serviceBinder.pbsSetIPD(var1, new Consumer<Integer>() {
                @Override
                public void accept(Integer integer) {
                    callback.CallBack(integer);
                }
            });
        } else {
            Log.e(TAG, "pbsSetIPD: not bind ToBService");
        }
    }
    public void pbsPropertySetPowerOnOffLogo(PBS_PowerOnOffLogoEnum var1, String path, int var3, BoolCallback callback) {
        if (serviceBinder != null) {
            try {
                serviceBinder.pbsPropertySetPowerOnOffLogo(var1, path, var3, new IBoolCallback.Stub() {
                    @Override
                    public void callBack(boolean b) throws RemoteException {
                        callback.CallBack(b);
                    }
                });
            } catch (RemoteException e) {
                throw new RuntimeException(e);
            }
        } else {
            Log.e(TAG, "pbsPropertySetPowerOnOffLogo: not bind ToBService");
        }
    }
    public int setPicoCastMediaFormat(int _bitrate, int ret) {
        if (serviceBinder != null) {
            PicoCastMediaFormat format = new PicoCastMediaFormat();
            format.bitrate = _bitrate;
            return serviceBinder.setPicoCastMediaFormat(format,ret);
        } else {
            Log.e(TAG, "setPicoCastMediaFormat: not bind ToBService");
        }
        return -1;
    }

    @TargetApi(Build.VERSION_CODES.N)
    public int setMarkerInfoCallback(StringCallback callback) {
        if (serviceBinder != null) {

            return serviceBinder.setMarkerInfoCallback(new Consumer<MarkerInfo[]>() {
                @Override
                public void accept(MarkerInfo[] markerInfos) {
                    try {
                        JSONArray result = new JSONArray();
                        for (MarkerInfo member : markerInfos) {
                            result.put(new JSONObject(member.toJson()));
                        }
                        callback.CallBack(result.toString());
                    } catch (JSONException e) {
                        throw new RuntimeException(e);
                    }
                }
            });
        } else {
            Log.e(TAG, "setMarkerInfoCallback: not bind ToBService");
        }
        return -1;
    }
}
