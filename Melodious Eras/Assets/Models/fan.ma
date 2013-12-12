//Maya ASCII 2014 scene
//Name: fan.ma
//Last modified: Wed, Dec 11, 2013 07:51:26 PM
//Codeset: 1252
requires maya "2014";
requires -nodeType "mentalrayFramebuffer" -nodeType "mentalrayOutputPass" -nodeType "mentalrayRenderPass"
		 -nodeType "mentalrayUserBuffer" -nodeType "mentalraySubdivApprox" -nodeType "mentalrayCurveApprox"
		 -nodeType "mentalraySurfaceApprox" -nodeType "mentalrayDisplaceApprox" -nodeType "mentalrayOptions"
		 -nodeType "mentalrayGlobals" -nodeType "mentalrayItemsList" -nodeType "mentalrayShader"
		 -nodeType "mentalrayUserData" -nodeType "mentalrayText" -nodeType "mentalrayTessellation"
		 -nodeType "mentalrayPhenomenon" -nodeType "mentalrayLightProfile" -nodeType "mentalrayVertexColors"
		 -nodeType "mentalrayIblShape" -nodeType "mapVizShape" -nodeType "mentalrayCCMeshProxy"
		 -nodeType "cylindricalLightLocator" -nodeType "discLightLocator" -nodeType "rectangularLightLocator"
		 -nodeType "sphericalLightLocator" -nodeType "abcimport" -nodeType "mia_physicalsun"
		 -nodeType "mia_physicalsky" -nodeType "mia_material" -nodeType "mia_material_x" -nodeType "mia_roundcorners"
		 -nodeType "mia_exposure_simple" -nodeType "mia_portal_light" -nodeType "mia_light_surface"
		 -nodeType "mia_exposure_photographic" -nodeType "mia_exposure_photographic_rev" -nodeType "mia_lens_bokeh"
		 -nodeType "mia_envblur" -nodeType "mia_ciesky" -nodeType "mia_photometric_light"
		 -nodeType "mib_texture_vector" -nodeType "mib_texture_remap" -nodeType "mib_texture_rotate"
		 -nodeType "mib_bump_basis" -nodeType "mib_bump_map" -nodeType "mib_passthrough_bump_map"
		 -nodeType "mib_bump_map2" -nodeType "mib_lookup_spherical" -nodeType "mib_lookup_cube1"
		 -nodeType "mib_lookup_cube6" -nodeType "mib_lookup_background" -nodeType "mib_lookup_cylindrical"
		 -nodeType "mib_texture_lookup" -nodeType "mib_texture_lookup2" -nodeType "mib_texture_filter_lookup"
		 -nodeType "mib_texture_checkerboard" -nodeType "mib_texture_polkadot" -nodeType "mib_texture_polkasphere"
		 -nodeType "mib_texture_turbulence" -nodeType "mib_texture_wave" -nodeType "mib_reflect"
		 -nodeType "mib_refract" -nodeType "mib_transparency" -nodeType "mib_continue" -nodeType "mib_opacity"
		 -nodeType "mib_twosided" -nodeType "mib_refraction_index" -nodeType "mib_dielectric"
		 -nodeType "mib_ray_marcher" -nodeType "mib_illum_lambert" -nodeType "mib_illum_phong"
		 -nodeType "mib_illum_ward" -nodeType "mib_illum_ward_deriv" -nodeType "mib_illum_blinn"
		 -nodeType "mib_illum_cooktorr" -nodeType "mib_illum_hair" -nodeType "mib_volume"
		 -nodeType "mib_color_alpha" -nodeType "mib_color_average" -nodeType "mib_color_intensity"
		 -nodeType "mib_color_interpolate" -nodeType "mib_color_mix" -nodeType "mib_color_spread"
		 -nodeType "mib_geo_cube" -nodeType "mib_geo_torus" -nodeType "mib_geo_sphere" -nodeType "mib_geo_cone"
		 -nodeType "mib_geo_cylinder" -nodeType "mib_geo_square" -nodeType "mib_geo_instance"
		 -nodeType "mib_geo_instance_mlist" -nodeType "mib_geo_add_uv_texsurf" -nodeType "mib_photon_basic"
		 -nodeType "mib_light_infinite" -nodeType "mib_light_point" -nodeType "mib_light_spot"
		 -nodeType "mib_light_photometric" -nodeType "mib_cie_d" -nodeType "mib_blackbody"
		 -nodeType "mib_shadow_transparency" -nodeType "mib_lens_stencil" -nodeType "mib_lens_clamp"
		 -nodeType "mib_lightmap_write" -nodeType "mib_lightmap_sample" -nodeType "mib_amb_occlusion"
		 -nodeType "mib_fast_occlusion" -nodeType "mib_map_get_scalar" -nodeType "mib_map_get_integer"
		 -nodeType "mib_map_get_vector" -nodeType "mib_map_get_color" -nodeType "mib_map_get_transform"
		 -nodeType "mib_map_get_scalar_array" -nodeType "mib_map_get_integer_array" -nodeType "mib_fg_occlusion"
		 -nodeType "mib_bent_normal_env" -nodeType "mib_glossy_reflection" -nodeType "mib_glossy_refraction"
		 -nodeType "builtin_bsdf_architectural" -nodeType "builtin_bsdf_architectural_comp"
		 -nodeType "builtin_bsdf_carpaint" -nodeType "builtin_bsdf_ashikhmin" -nodeType "builtin_bsdf_lambert"
		 -nodeType "builtin_bsdf_mirror" -nodeType "builtin_bsdf_phong" -nodeType "contour_store_function"
		 -nodeType "contour_store_function_simple" -nodeType "contour_contrast_function_levels"
		 -nodeType "contour_contrast_function_simple" -nodeType "contour_shader_simple" -nodeType "contour_shader_silhouette"
		 -nodeType "contour_shader_maxcolor" -nodeType "contour_shader_curvature" -nodeType "contour_shader_factorcolor"
		 -nodeType "contour_shader_depthfade" -nodeType "contour_shader_framefade" -nodeType "contour_shader_layerthinner"
		 -nodeType "contour_shader_widthfromcolor" -nodeType "contour_shader_widthfromlightdir"
		 -nodeType "contour_shader_widthfromlight" -nodeType "contour_shader_combi" -nodeType "contour_only"
		 -nodeType "contour_composite" -nodeType "contour_ps" -nodeType "mi_metallic_paint"
		 -nodeType "mi_metallic_paint_x" -nodeType "mi_bump_flakes" -nodeType "mi_car_paint_phen"
		 -nodeType "mi_metallic_paint_output_mixer" -nodeType "mi_car_paint_phen_x" -nodeType "physical_lens_dof"
		 -nodeType "physical_light" -nodeType "dgs_material" -nodeType "dgs_material_photon"
		 -nodeType "dielectric_material" -nodeType "dielectric_material_photon" -nodeType "oversampling_lens"
		 -nodeType "path_material" -nodeType "parti_volume" -nodeType "parti_volume_photon"
		 -nodeType "transmat" -nodeType "transmat_photon" -nodeType "mip_rayswitch" -nodeType "mip_rayswitch_advanced"
		 -nodeType "mip_rayswitch_environment" -nodeType "mip_card_opacity" -nodeType "mip_motionblur"
		 -nodeType "mip_motion_vector" -nodeType "mip_matteshadow" -nodeType "mip_cameramap"
		 -nodeType "mip_mirrorball" -nodeType "mip_grayball" -nodeType "mip_gamma_gain" -nodeType "mip_render_subset"
		 -nodeType "mip_matteshadow_mtl" -nodeType "mip_binaryproxy" -nodeType "mip_rayswitch_stage"
		 -nodeType "mip_fgshooter" -nodeType "mib_ptex_lookup" -nodeType "misss_physical"
		 -nodeType "misss_physical_phen" -nodeType "misss_fast_shader" -nodeType "misss_fast_shader_x"
		 -nodeType "misss_fast_shader2" -nodeType "misss_fast_shader2_x" -nodeType "misss_skin_specular"
		 -nodeType "misss_lightmap_write" -nodeType "misss_lambert_gamma" -nodeType "misss_call_shader"
		 -nodeType "misss_set_normal" -nodeType "misss_fast_lmap_maya" -nodeType "misss_fast_simple_maya"
		 -nodeType "misss_fast_skin_maya" -nodeType "misss_fast_skin_phen" -nodeType "misss_fast_skin_phen_d"
		 -nodeType "misss_mia_skin2_phen" -nodeType "misss_mia_skin2_phen_d" -nodeType "misss_lightmap_phen"
		 -nodeType "misss_mia_skin2_surface_phen" -nodeType "surfaceSampler" -nodeType "mib_data_bool"
		 -nodeType "mib_data_int" -nodeType "mib_data_scalar" -nodeType "mib_data_vector"
		 -nodeType "mib_data_color" -nodeType "mib_data_string" -nodeType "mib_data_texture"
		 -nodeType "mib_data_shader" -nodeType "mib_data_bool_array" -nodeType "mib_data_int_array"
		 -nodeType "mib_data_scalar_array" -nodeType "mib_data_vector_array" -nodeType "mib_data_color_array"
		 -nodeType "mib_data_string_array" -nodeType "mib_data_texture_array" -nodeType "mib_data_shader_array"
		 -nodeType "mib_data_get_bool" -nodeType "mib_data_get_int" -nodeType "mib_data_get_scalar"
		 -nodeType "mib_data_get_vector" -nodeType "mib_data_get_color" -nodeType "mib_data_get_string"
		 -nodeType "mib_data_get_texture" -nodeType "mib_data_get_shader" -nodeType "mib_data_get_shader_bool"
		 -nodeType "mib_data_get_shader_int" -nodeType "mib_data_get_shader_scalar" -nodeType "mib_data_get_shader_vector"
		 -nodeType "mib_data_get_shader_color" -nodeType "user_ibl_env" -nodeType "user_ibl_rect"
		 -nodeType "mia_material_x_passes" -nodeType "mi_metallic_paint_x_passes" -nodeType "mi_car_paint_phen_x_passes"
		 -nodeType "misss_fast_shader_x_passes" -dataType "byteArray" "Mayatomr" "2014.0 - 3.11.1.9 ";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2014";
fileInfo "version" "2014";
fileInfo "cutIdentifier" "201307170459-880822";
fileInfo "osv" "Microsoft Windows 8 Home Premium Edition, 64-bit  (Build 9200)\n";
fileInfo "license" "student";
createNode transform -s -n "persp";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -12.356565925067848 -6.8072508688208568 7.6418878198003757 ;
	setAttr ".r" -type "double3" -202.53835273265966 842.99999999996987 0 ;
createNode camera -s -n "perspShape" -p "persp";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".coi" 15.091115858940789;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".hc" -type "string" "viewSet -p %camera";
createNode transform -s -n "top";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 100.1 0 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
createNode transform -s -n "front";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 100.1 ;
createNode camera -s -n "frontShape" -p "front";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
createNode transform -s -n "side";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 100.1 0 0 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
createNode transform -n "pCylinder1";
	setAttr ".t" -type "double3" 0 -5.1962756527426395 0 ;
	setAttr ".s" -type "double3" 1 1.0159385517748969 1 ;
createNode transform -n "polySurface2" -p "pCylinder1";
createNode mesh -n "polySurfaceShape2" -p "polySurface2";
	setAttr -k off ".v";
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 1;
createNode transform -n "polySurface3" -p "pCylinder1";
	setAttr ".t" -type "double3" 0 -0.010212600255352679 0 ;
	setAttr ".rp" -type "double3" 0 3.636614203453064 0 ;
	setAttr ".sp" -type "double3" 0 3.636614203453064 0 ;
createNode mesh -n "polySurfaceShape3" -p "polySurface3";
	setAttr -k off ".v";
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 1;
createNode transform -n "transform5" -p "pCylinder1";
	setAttr ".v" no;
createNode mesh -n "pCylinderShape1" -p "transform5";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 3;
	setAttr ".dsm" 2;
createNode transform -n "group1";
	setAttr ".t" -type "double3" 0 0.2964099400220741 -0.024128491408865749 ;
	setAttr ".s" -type "double3" 1.1223135994237419 1.1223135994237419 1.1223135994237419 ;
createNode transform -n "pCube1" -p "group1";
	setAttr ".t" -type "double3" -3.4601346911676134 -2.0025013819502675 -0.15436410782138232 ;
	setAttr ".s" -type "double3" 4.7832452807467991 0.064072636393356475 1.75 ;
createNode transform -n "transform1" -p "pCube1";
	setAttr ".v" no;
createNode mesh -n "pCubeShape1" -p "transform1";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 16 ".pt";
	setAttr ".pt[0]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[1]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[2]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[3]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[4]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[5]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[6]" -type "float3" 0 0 -0.64896148 ;
	setAttr ".pt[7]" -type "float3" 0 0 -0.64896148 ;
	setAttr ".pt[13]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[14]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[18]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[19]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[22]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[23]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[24]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[25]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".dr" 1;
createNode transform -n "pCube5" -p "group1";
	setAttr ".t" -type "double3" 0.063885431412877849 -2.0025013819502679 -3.5373703721513774 ;
	setAttr ".r" -type "double3" 0 -90 0 ;
	setAttr ".s" -type "double3" 4.7832452807467991 0.064072636393356475 1.75 ;
createNode transform -n "transform2" -p "pCube5";
	setAttr ".v" no;
createNode mesh -n "pCubeShape5" -p "transform2";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".iog[0].og[0].gcl" -type "componentList" 1 "f[0:20]";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 49 ".uvst[0].uvsp[0:48]" -type "float2" 0.375 0 0.24998748
		 0.24999949 0.24998751 -4.5448542e-007 0.37499994 0.24999949 0.3880651 0.50000024
		 0.3880651 0.75000072 0.38806513 0.75000024 0.40062749 0.75000095 0.38806507 0.50000072
		 0.40062749 0.50000095 0.5 -4.7683534e-007 0.62413341 -4.7683534e-007 0.6250003 0.18750548
		 0.50000012 0.24999894 0.6241343 0.24999903 0.38806435 0.43750677 0.37499887 0.37501246
		 0.40113026 0.56249464 0.62500006 0.062492609 0.87106287 0.062492721 0.87106287 0.18750583
		 0.40062779 0.75240576 0.62418348 0.75400507 0.62413239 0.99999905 0.50000006 0.99999899
		 0.37500101 0.99999893 0.37500101 0.87498748 0.3880659 0.81249428 0.62500048 0.24999952
		 0.6241979 0.56249464 0.62500036 0.54961073 0.62418485 0.49599677 0.40113026 0.50000048
		 0.4006272 0.49759606 0.87094885 0 0.625 0 0.62419772 0.68750775 0.40113026 0.75000048
		 0.40113026 0.68750775 0.62500048 0.50000048 0.62500048 0.50000048 0.62500018 0.65013802
		 0.625 0.75000048 0.51266396 0.68750775 0.51266408 0.56249464 0.51306534 0.50000048
		 0.5123893 0.49679655 0.5130651 0.75000048 0.51240563 0.75320542;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 16 ".pt";
	setAttr ".pt[0]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[1]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[2]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[3]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[4]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[5]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[6]" -type "float3" 0 0 -0.64896148 ;
	setAttr ".pt[7]" -type "float3" 0 0 -0.64896148 ;
	setAttr ".pt[13]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[14]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[18]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[19]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[22]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[23]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[24]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[25]" -type "float3" 0 0 -0.52569425 ;
	setAttr -s 26 ".vt[0:25]"  -0.44453943 -0.50000763 0.44503251 -0.44453943 0.49999619 0.44503251
		 -0.5 0.49999619 -4.9999999e-005 -0.5 -0.50000763 -4.9999999e-005 -0.4759903 0.49999619 -0.3421078
		 -0.4759903 -0.50000763 -0.34210777 0.017026663 -0.50000763 0.30501714 0.017026663 0.49999619 0.30501714
		 0.45373961 0.33808506 0.24911523 0.45708802 0.088109836 0.24911523 0.54337358 0.25002098 -0.46859759
		 0.5322265 0.25002098 -0.51619607 0.52946877 0.49999619 -0.46778738 -0.39547873 0.25002098 -0.5
		 -0.39857697 0.49999619 -0.49392408 0.54337358 -0.25003242 -0.46859759 0.52946877 -0.50000763 -0.46778738
		 0.5322265 -0.25003242 -0.51619607 -0.39857697 -0.50000763 -0.49392408 -0.39547873 -0.25003242 -0.5
		 0.45373961 -0.66191876 0.24911523 0.45708802 -0.41194355 0.24911523 0.065376312 0.49999619 -0.4808577
		 0.0654459 -0.50000763 -0.48085573 0.068373889 0.25002098 -0.50809801 0.068373889 -0.25003242 -0.50809801;
	setAttr -s 45 ".ed[0:44]"  6 7 0 0 6 0 1 7 0 7 8 0 8 12 0 12 22 0 0 1 0
		 1 2 0 2 3 0 18 23 0 16 20 0 0 3 0 3 5 0 5 18 0 2 4 0 4 5 0 6 20 0 20 21 0 4 14 0
		 13 19 1 13 24 0 11 17 0 17 25 0 9 21 0 8 9 0 9 10 0 10 12 0 10 15 0 15 17 0 11 24 0
		 13 14 0 14 22 0 15 21 0 16 23 0 18 19 0 19 25 0 10 11 0 11 12 0 15 16 0 16 17 0 6 23 0
		 23 25 0 24 25 0 22 24 0 7 22 0;
	setAttr -s 21 -ch 90 ".fc[0:20]" -type "polyFaces" 
		f 4 1 0 -3 -7
		mu 0 4 0 10 13 3
		f 4 -12 6 7 8
		mu 0 4 2 0 3 1
		f 4 -9 14 15 -13
		mu 0 4 6 4 8 5
		f 6 -4 -1 16 17 -24 -25
		mu 0 6 14 13 10 11 18 12
		f 6 -14 -16 18 -31 19 -35
		mu 0 6 7 5 8 9 17 38
		f 4 -43 -30 21 22
		mu 0 4 43 44 29 36
		f 4 23 -33 -28 -26
		mu 0 4 12 18 19 20
		f 4 24 25 26 -5
		mu 0 4 14 28 39 31
		f 4 -22 -37 27 28
		mu 0 4 36 29 30 41
		f 4 -44 -6 -38 29
		mu 0 4 45 46 31 40
		f 4 -39 32 -18 -11
		mu 0 4 34 19 18 35
		f 4 41 -23 -40 33
		mu 0 4 47 43 36 42
		f 3 36 37 -27
		mu 0 3 39 40 31
		f 3 38 39 -29
		mu 0 3 41 42 36
		f 4 -45 3 4 5
		mu 0 4 46 13 14 31
		f 4 40 -34 10 -17
		mu 0 4 24 48 22 23
		f 6 -41 -2 11 12 13 9
		mu 0 6 48 24 25 26 27 21
		f 4 -42 -10 34 35
		mu 0 4 43 47 37 38
		f 4 42 -36 -20 20
		mu 0 4 44 43 38 17
		f 4 43 -21 30 31
		mu 0 4 46 45 32 33
		f 6 44 -32 -19 -15 -8 2
		mu 0 6 13 46 33 15 16 3;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".dr" 1;
createNode transform -n "pCube6" -p "group1";
	setAttr ".t" -type "double3" 3.4769467265153864 -2.0025013819502675 0.03566706658725971 ;
	setAttr ".r" -type "double3" 0 -180 0 ;
	setAttr ".s" -type "double3" 4.7832452807467991 0.064072636393356475 1.75 ;
createNode transform -n "transform3" -p "pCube6";
	setAttr ".v" no;
createNode mesh -n "pCubeShape6" -p "transform3";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".iog[0].og[0].gcl" -type "componentList" 1 "f[0:20]";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 49 ".uvst[0].uvsp[0:48]" -type "float2" 0.375 0 0.24998748
		 0.24999949 0.24998751 -4.5448542e-007 0.37499994 0.24999949 0.3880651 0.50000024
		 0.3880651 0.75000072 0.38806513 0.75000024 0.40062749 0.75000095 0.38806507 0.50000072
		 0.40062749 0.50000095 0.5 -4.7683534e-007 0.62413341 -4.7683534e-007 0.6250003 0.18750548
		 0.50000012 0.24999894 0.6241343 0.24999903 0.38806435 0.43750677 0.37499887 0.37501246
		 0.40113026 0.56249464 0.62500006 0.062492609 0.87106287 0.062492721 0.87106287 0.18750583
		 0.40062779 0.75240576 0.62418348 0.75400507 0.62413239 0.99999905 0.50000006 0.99999899
		 0.37500101 0.99999893 0.37500101 0.87498748 0.3880659 0.81249428 0.62500048 0.24999952
		 0.6241979 0.56249464 0.62500036 0.54961073 0.62418485 0.49599677 0.40113026 0.50000048
		 0.4006272 0.49759606 0.87094885 0 0.625 0 0.62419772 0.68750775 0.40113026 0.75000048
		 0.40113026 0.68750775 0.62500048 0.50000048 0.62500048 0.50000048 0.62500018 0.65013802
		 0.625 0.75000048 0.51266396 0.68750775 0.51266408 0.56249464 0.51306534 0.50000048
		 0.5123893 0.49679655 0.5130651 0.75000048 0.51240563 0.75320542;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 16 ".pt";
	setAttr ".pt[0]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[1]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[2]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[3]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[4]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[5]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[6]" -type "float3" 0 0 -0.64896148 ;
	setAttr ".pt[7]" -type "float3" 0 0 -0.64896148 ;
	setAttr ".pt[13]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[14]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[18]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[19]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[22]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[23]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[24]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[25]" -type "float3" 0 0 -0.52569425 ;
	setAttr -s 26 ".vt[0:25]"  -0.44453943 -0.50000763 0.44503251 -0.44453943 0.49999619 0.44503251
		 -0.5 0.49999619 -4.9999999e-005 -0.5 -0.50000763 -4.9999999e-005 -0.4759903 0.49999619 -0.3421078
		 -0.4759903 -0.50000763 -0.34210777 0.017026663 -0.50000763 0.30501714 0.017026663 0.49999619 0.30501714
		 0.45373961 0.33808506 0.24911523 0.45708802 0.088109836 0.24911523 0.54337358 0.25002098 -0.46859759
		 0.5322265 0.25002098 -0.51619607 0.52946877 0.49999619 -0.46778738 -0.39547873 0.25002098 -0.5
		 -0.39857697 0.49999619 -0.49392408 0.54337358 -0.25003242 -0.46859759 0.52946877 -0.50000763 -0.46778738
		 0.5322265 -0.25003242 -0.51619607 -0.39857697 -0.50000763 -0.49392408 -0.39547873 -0.25003242 -0.5
		 0.45373961 -0.66191876 0.24911523 0.45708802 -0.41194355 0.24911523 0.065376312 0.49999619 -0.4808577
		 0.0654459 -0.50000763 -0.48085573 0.068373889 0.25002098 -0.50809801 0.068373889 -0.25003242 -0.50809801;
	setAttr -s 45 ".ed[0:44]"  6 7 0 0 6 0 1 7 0 7 8 0 8 12 0 12 22 0 0 1 0
		 1 2 0 2 3 0 18 23 0 16 20 0 0 3 0 3 5 0 5 18 0 2 4 0 4 5 0 6 20 0 20 21 0 4 14 0
		 13 19 1 13 24 0 11 17 0 17 25 0 9 21 0 8 9 0 9 10 0 10 12 0 10 15 0 15 17 0 11 24 0
		 13 14 0 14 22 0 15 21 0 16 23 0 18 19 0 19 25 0 10 11 0 11 12 0 15 16 0 16 17 0 6 23 0
		 23 25 0 24 25 0 22 24 0 7 22 0;
	setAttr -s 21 -ch 90 ".fc[0:20]" -type "polyFaces" 
		f 4 1 0 -3 -7
		mu 0 4 0 10 13 3
		f 4 -12 6 7 8
		mu 0 4 2 0 3 1
		f 4 -9 14 15 -13
		mu 0 4 6 4 8 5
		f 6 -4 -1 16 17 -24 -25
		mu 0 6 14 13 10 11 18 12
		f 6 -14 -16 18 -31 19 -35
		mu 0 6 7 5 8 9 17 38
		f 4 -43 -30 21 22
		mu 0 4 43 44 29 36
		f 4 23 -33 -28 -26
		mu 0 4 12 18 19 20
		f 4 24 25 26 -5
		mu 0 4 14 28 39 31
		f 4 -22 -37 27 28
		mu 0 4 36 29 30 41
		f 4 -44 -6 -38 29
		mu 0 4 45 46 31 40
		f 4 -39 32 -18 -11
		mu 0 4 34 19 18 35
		f 4 41 -23 -40 33
		mu 0 4 47 43 36 42
		f 3 36 37 -27
		mu 0 3 39 40 31
		f 3 38 39 -29
		mu 0 3 41 42 36
		f 4 -45 3 4 5
		mu 0 4 46 13 14 31
		f 4 40 -34 10 -17
		mu 0 4 24 48 22 23
		f 6 -41 -2 11 12 13 9
		mu 0 6 48 24 25 26 27 21
		f 4 -42 -10 34 35
		mu 0 4 43 47 37 38
		f 4 42 -36 -20 20
		mu 0 4 44 43 38 17
		f 4 43 -21 30 31
		mu 0 4 46 45 32 33
		f 6 44 -32 -19 -15 -8 2
		mu 0 6 13 46 33 15 16 3;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".dr" 1;
createNode transform -n "pCube4" -p "group1";
	setAttr ".t" -type "double3" -0.40786874660237865 -2.0025013819502675 3.3274234882063323 ;
	setAttr ".r" -type "double3" 0 90 0 ;
	setAttr ".s" -type "double3" 4.7832452807467991 0.064072636393356475 1.75 ;
createNode transform -n "transform4" -p "pCube4";
	setAttr ".v" no;
createNode mesh -n "pCubeShape4" -p "transform4";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".iog[0].og[0].gcl" -type "componentList" 1 "f[0:20]";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 49 ".uvst[0].uvsp[0:48]" -type "float2" 0.375 0 0.24998748
		 0.24999949 0.24998751 -4.5448542e-007 0.37499994 0.24999949 0.3880651 0.50000024
		 0.3880651 0.75000072 0.38806513 0.75000024 0.40062749 0.75000095 0.38806507 0.50000072
		 0.40062749 0.50000095 0.5 -4.7683534e-007 0.62413341 -4.7683534e-007 0.6250003 0.18750548
		 0.50000012 0.24999894 0.6241343 0.24999903 0.38806435 0.43750677 0.37499887 0.37501246
		 0.40113026 0.56249464 0.62500006 0.062492609 0.87106287 0.062492721 0.87106287 0.18750583
		 0.40062779 0.75240576 0.62418348 0.75400507 0.62413239 0.99999905 0.50000006 0.99999899
		 0.37500101 0.99999893 0.37500101 0.87498748 0.3880659 0.81249428 0.62500048 0.24999952
		 0.6241979 0.56249464 0.62500036 0.54961073 0.62418485 0.49599677 0.40113026 0.50000048
		 0.4006272 0.49759606 0.87094885 0 0.625 0 0.62419772 0.68750775 0.40113026 0.75000048
		 0.40113026 0.68750775 0.62500048 0.50000048 0.62500048 0.50000048 0.62500018 0.65013802
		 0.625 0.75000048 0.51266396 0.68750775 0.51266408 0.56249464 0.51306534 0.50000048
		 0.5123893 0.49679655 0.5130651 0.75000048 0.51240563 0.75320542;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 16 ".pt";
	setAttr ".pt[0]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[1]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[2]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[3]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[4]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[5]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[6]" -type "float3" 0 0 -0.64896148 ;
	setAttr ".pt[7]" -type "float3" 0 0 -0.64896148 ;
	setAttr ".pt[13]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[14]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[18]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[19]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[22]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[23]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[24]" -type "float3" 0 0 -0.52569425 ;
	setAttr ".pt[25]" -type "float3" 0 0 -0.52569425 ;
	setAttr -s 26 ".vt[0:25]"  -0.44453943 -0.50000763 0.44503251 -0.44453943 0.49999619 0.44503251
		 -0.5 0.49999619 -4.9999999e-005 -0.5 -0.50000763 -4.9999999e-005 -0.4759903 0.49999619 -0.3421078
		 -0.4759903 -0.50000763 -0.34210777 0.017026663 -0.50000763 0.30501714 0.017026663 0.49999619 0.30501714
		 0.45373961 0.33808506 0.24911523 0.45708802 0.088109836 0.24911523 0.54337358 0.25002098 -0.46859759
		 0.5322265 0.25002098 -0.51619607 0.52946877 0.49999619 -0.46778738 -0.39547873 0.25002098 -0.5
		 -0.39857697 0.49999619 -0.49392408 0.54337358 -0.25003242 -0.46859759 0.52946877 -0.50000763 -0.46778738
		 0.5322265 -0.25003242 -0.51619607 -0.39857697 -0.50000763 -0.49392408 -0.39547873 -0.25003242 -0.5
		 0.45373961 -0.66191876 0.24911523 0.45708802 -0.41194355 0.24911523 0.065376312 0.49999619 -0.4808577
		 0.0654459 -0.50000763 -0.48085573 0.068373889 0.25002098 -0.50809801 0.068373889 -0.25003242 -0.50809801;
	setAttr -s 45 ".ed[0:44]"  6 7 0 0 6 0 1 7 0 7 8 0 8 12 0 12 22 0 0 1 0
		 1 2 0 2 3 0 18 23 0 16 20 0 0 3 0 3 5 0 5 18 0 2 4 0 4 5 0 6 20 0 20 21 0 4 14 0
		 13 19 1 13 24 0 11 17 0 17 25 0 9 21 0 8 9 0 9 10 0 10 12 0 10 15 0 15 17 0 11 24 0
		 13 14 0 14 22 0 15 21 0 16 23 0 18 19 0 19 25 0 10 11 0 11 12 0 15 16 0 16 17 0 6 23 0
		 23 25 0 24 25 0 22 24 0 7 22 0;
	setAttr -s 21 -ch 90 ".fc[0:20]" -type "polyFaces" 
		f 4 1 0 -3 -7
		mu 0 4 0 10 13 3
		f 4 -12 6 7 8
		mu 0 4 2 0 3 1
		f 4 -9 14 15 -13
		mu 0 4 6 4 8 5
		f 6 -4 -1 16 17 -24 -25
		mu 0 6 14 13 10 11 18 12
		f 6 -14 -16 18 -31 19 -35
		mu 0 6 7 5 8 9 17 38
		f 4 -43 -30 21 22
		mu 0 4 43 44 29 36
		f 4 23 -33 -28 -26
		mu 0 4 12 18 19 20
		f 4 24 25 26 -5
		mu 0 4 14 28 39 31
		f 4 -22 -37 27 28
		mu 0 4 36 29 30 41
		f 4 -44 -6 -38 29
		mu 0 4 45 46 31 40
		f 4 -39 32 -18 -11
		mu 0 4 34 19 18 35
		f 4 41 -23 -40 33
		mu 0 4 47 43 36 42
		f 3 36 37 -27
		mu 0 3 39 40 31
		f 3 38 39 -29
		mu 0 3 41 42 36
		f 4 -45 3 4 5
		mu 0 4 46 13 14 31
		f 4 40 -34 10 -17
		mu 0 4 24 48 22 23
		f 6 -41 -2 11 12 13 9
		mu 0 6 48 24 25 26 27 21
		f 4 -42 -10 34 35
		mu 0 4 43 47 37 38
		f 4 42 -36 -20 20
		mu 0 4 44 43 38 17
		f 4 43 -21 30 31
		mu 0 4 46 45 32 33
		f 6 44 -32 -19 -15 -8 2
		mu 0 6 13 46 33 15 16 3;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".dr" 1;
createNode transform -n "polySurface1";
createNode mesh -n "polySurfaceShape1" -p "polySurface1";
	setAttr -k off ".v";
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 1;
createNode lightLinker -s -n "lightLinker1";
	setAttr -s 2 ".lnk";
	setAttr -s 2 ".slnk";
createNode displayLayerManager -n "layerManager";
createNode displayLayer -n "defaultLayer";
createNode renderLayerManager -n "renderLayerManager";
createNode renderLayer -n "defaultRenderLayer";
	setAttr ".g" yes;
createNode mentalrayItemsList -s -n "mentalrayItemsList";
createNode mentalrayGlobals -s -n "mentalrayGlobals";
createNode mentalrayOptions -s -n "miDefaultOptions";
	addAttr -ci true -m -sn "stringOptions" -ln "stringOptions" -at "compound" -nc 
		3;
	addAttr -ci true -sn "name" -ln "name" -dt "string" -p "stringOptions";
	addAttr -ci true -sn "value" -ln "value" -dt "string" -p "stringOptions";
	addAttr -ci true -sn "type" -ln "type" -dt "string" -p "stringOptions";
	setAttr -s 45 ".stringOptions";
	setAttr ".stringOptions[0].name" -type "string" "rast motion factor";
	setAttr ".stringOptions[0].value" -type "string" "1.0";
	setAttr ".stringOptions[0].type" -type "string" "scalar";
	setAttr ".stringOptions[1].name" -type "string" "rast transparency depth";
	setAttr ".stringOptions[1].value" -type "string" "8";
	setAttr ".stringOptions[1].type" -type "string" "integer";
	setAttr ".stringOptions[2].name" -type "string" "rast useopacity";
	setAttr ".stringOptions[2].value" -type "string" "true";
	setAttr ".stringOptions[2].type" -type "string" "boolean";
	setAttr ".stringOptions[3].name" -type "string" "importon";
	setAttr ".stringOptions[3].value" -type "string" "false";
	setAttr ".stringOptions[3].type" -type "string" "boolean";
	setAttr ".stringOptions[4].name" -type "string" "importon density";
	setAttr ".stringOptions[4].value" -type "string" "1.0";
	setAttr ".stringOptions[4].type" -type "string" "scalar";
	setAttr ".stringOptions[5].name" -type "string" "importon merge";
	setAttr ".stringOptions[5].value" -type "string" "0.0";
	setAttr ".stringOptions[5].type" -type "string" "scalar";
	setAttr ".stringOptions[6].name" -type "string" "importon trace depth";
	setAttr ".stringOptions[6].value" -type "string" "0";
	setAttr ".stringOptions[6].type" -type "string" "integer";
	setAttr ".stringOptions[7].name" -type "string" "importon traverse";
	setAttr ".stringOptions[7].value" -type "string" "true";
	setAttr ".stringOptions[7].type" -type "string" "boolean";
	setAttr ".stringOptions[8].name" -type "string" "shadowmap pixel samples";
	setAttr ".stringOptions[8].value" -type "string" "3";
	setAttr ".stringOptions[8].type" -type "string" "integer";
	setAttr ".stringOptions[9].name" -type "string" "ambient occlusion";
	setAttr ".stringOptions[9].value" -type "string" "false";
	setAttr ".stringOptions[9].type" -type "string" "boolean";
	setAttr ".stringOptions[10].name" -type "string" "ambient occlusion rays";
	setAttr ".stringOptions[10].value" -type "string" "256";
	setAttr ".stringOptions[10].type" -type "string" "integer";
	setAttr ".stringOptions[11].name" -type "string" "ambient occlusion cache";
	setAttr ".stringOptions[11].value" -type "string" "false";
	setAttr ".stringOptions[11].type" -type "string" "boolean";
	setAttr ".stringOptions[12].name" -type "string" "ambient occlusion cache density";
	setAttr ".stringOptions[12].value" -type "string" "1.0";
	setAttr ".stringOptions[12].type" -type "string" "scalar";
	setAttr ".stringOptions[13].name" -type "string" "ambient occlusion cache points";
	setAttr ".stringOptions[13].value" -type "string" "64";
	setAttr ".stringOptions[13].type" -type "string" "integer";
	setAttr ".stringOptions[14].name" -type "string" "irradiance particles";
	setAttr ".stringOptions[14].value" -type "string" "false";
	setAttr ".stringOptions[14].type" -type "string" "boolean";
	setAttr ".stringOptions[15].name" -type "string" "irradiance particles rays";
	setAttr ".stringOptions[15].value" -type "string" "256";
	setAttr ".stringOptions[15].type" -type "string" "integer";
	setAttr ".stringOptions[16].name" -type "string" "irradiance particles interpolate";
	setAttr ".stringOptions[16].value" -type "string" "1";
	setAttr ".stringOptions[16].type" -type "string" "integer";
	setAttr ".stringOptions[17].name" -type "string" "irradiance particles interppoints";
	setAttr ".stringOptions[17].value" -type "string" "64";
	setAttr ".stringOptions[17].type" -type "string" "integer";
	setAttr ".stringOptions[18].name" -type "string" "irradiance particles indirect passes";
	setAttr ".stringOptions[18].value" -type "string" "0";
	setAttr ".stringOptions[18].type" -type "string" "integer";
	setAttr ".stringOptions[19].name" -type "string" "irradiance particles scale";
	setAttr ".stringOptions[19].value" -type "string" "1.0";
	setAttr ".stringOptions[19].type" -type "string" "scalar";
	setAttr ".stringOptions[20].name" -type "string" "irradiance particles env";
	setAttr ".stringOptions[20].value" -type "string" "true";
	setAttr ".stringOptions[20].type" -type "string" "boolean";
	setAttr ".stringOptions[21].name" -type "string" "irradiance particles env rays";
	setAttr ".stringOptions[21].value" -type "string" "256";
	setAttr ".stringOptions[21].type" -type "string" "integer";
	setAttr ".stringOptions[22].name" -type "string" "irradiance particles env scale";
	setAttr ".stringOptions[22].value" -type "string" "1";
	setAttr ".stringOptions[22].type" -type "string" "integer";
	setAttr ".stringOptions[23].name" -type "string" "irradiance particles rebuild";
	setAttr ".stringOptions[23].value" -type "string" "true";
	setAttr ".stringOptions[23].type" -type "string" "boolean";
	setAttr ".stringOptions[24].name" -type "string" "irradiance particles file";
	setAttr ".stringOptions[24].value" -type "string" "";
	setAttr ".stringOptions[24].type" -type "string" "string";
	setAttr ".stringOptions[25].name" -type "string" "geom displace motion factor";
	setAttr ".stringOptions[25].value" -type "string" "1.0";
	setAttr ".stringOptions[25].type" -type "string" "scalar";
	setAttr ".stringOptions[26].name" -type "string" "contrast all buffers";
	setAttr ".stringOptions[26].value" -type "string" "true";
	setAttr ".stringOptions[26].type" -type "string" "boolean";
	setAttr ".stringOptions[27].name" -type "string" "finalgather normal tolerance";
	setAttr ".stringOptions[27].value" -type "string" "25.842";
	setAttr ".stringOptions[27].type" -type "string" "scalar";
	setAttr ".stringOptions[28].name" -type "string" "trace camera clip";
	setAttr ".stringOptions[28].value" -type "string" "false";
	setAttr ".stringOptions[28].type" -type "string" "boolean";
	setAttr ".stringOptions[29].name" -type "string" "unified sampling";
	setAttr ".stringOptions[29].value" -type "string" "true";
	setAttr ".stringOptions[29].type" -type "string" "boolean";
	setAttr ".stringOptions[30].name" -type "string" "samples quality";
	setAttr ".stringOptions[30].value" -type "string" "0.25 0.25 0.25 0.25";
	setAttr ".stringOptions[30].type" -type "string" "color";
	setAttr ".stringOptions[31].name" -type "string" "samples min";
	setAttr ".stringOptions[31].value" -type "string" "1.0";
	setAttr ".stringOptions[31].type" -type "string" "scalar";
	setAttr ".stringOptions[32].name" -type "string" "samples max";
	setAttr ".stringOptions[32].value" -type "string" "100.0";
	setAttr ".stringOptions[32].type" -type "string" "scalar";
	setAttr ".stringOptions[33].name" -type "string" "samples error cutoff";
	setAttr ".stringOptions[33].value" -type "string" "0.0 0.0 0.0 0.0";
	setAttr ".stringOptions[33].type" -type "string" "color";
	setAttr ".stringOptions[34].name" -type "string" "samples per object";
	setAttr ".stringOptions[34].value" -type "string" "false";
	setAttr ".stringOptions[34].type" -type "string" "boolean";
	setAttr ".stringOptions[35].name" -type "string" "progressive";
	setAttr ".stringOptions[35].value" -type "string" "false";
	setAttr ".stringOptions[35].type" -type "string" "boolean";
	setAttr ".stringOptions[36].name" -type "string" "progressive max time";
	setAttr ".stringOptions[36].value" -type "string" "0";
	setAttr ".stringOptions[36].type" -type "string" "integer";
	setAttr ".stringOptions[37].name" -type "string" "progressive subsampling size";
	setAttr ".stringOptions[37].value" -type "string" "1";
	setAttr ".stringOptions[37].type" -type "string" "integer";
	setAttr ".stringOptions[38].name" -type "string" "iray";
	setAttr ".stringOptions[38].value" -type "string" "false";
	setAttr ".stringOptions[38].type" -type "string" "boolean";
	setAttr ".stringOptions[39].name" -type "string" "light relative scale";
	setAttr ".stringOptions[39].value" -type "string" "0.31831";
	setAttr ".stringOptions[39].type" -type "string" "scalar";
	setAttr ".stringOptions[40].name" -type "string" "trace camera motion vectors";
	setAttr ".stringOptions[40].value" -type "string" "false";
	setAttr ".stringOptions[40].type" -type "string" "boolean";
	setAttr ".stringOptions[41].name" -type "string" "ray differentials";
	setAttr ".stringOptions[41].value" -type "string" "true";
	setAttr ".stringOptions[41].type" -type "string" "boolean";
	setAttr ".stringOptions[42].name" -type "string" "environment lighting mode";
	setAttr ".stringOptions[42].value" -type "string" "off";
	setAttr ".stringOptions[42].type" -type "string" "string";
	setAttr ".stringOptions[43].name" -type "string" "environment lighting quality";
	setAttr ".stringOptions[43].value" -type "string" "0.167";
	setAttr ".stringOptions[43].type" -type "string" "scalar";
	setAttr ".stringOptions[44].name" -type "string" "environment lighting shadow";
	setAttr ".stringOptions[44].value" -type "string" "transparent";
	setAttr ".stringOptions[44].type" -type "string" "string";
createNode mentalrayFramebuffer -s -n "miDefaultFramebuffer";
createNode polyCylinder -n "polyCylinder1";
	setAttr ".sc" 1;
	setAttr ".cuv" 3;
createNode polySplitRing -n "polySplitRing1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[40:59]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".wt" 0.69450646638870239;
	setAttr ".dr" no;
	setAttr ".re" 54;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[100:101]" "e[103]" "e[105]" "e[107]" "e[109]" "e[111]" "e[113]" "e[115]" "e[117]" "e[119]" "e[121]" "e[123]" "e[125]" "e[127]" "e[129]" "e[131]" "e[133]" "e[135]" "e[137]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".wt" 0.6597825288772583;
	setAttr ".dr" no;
	setAttr ".re" 101;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyTweak -n "polyTweak1";
	setAttr ".uopa" yes;
	setAttr -s 41 ".tk";
	setAttr ".tk[20]" -type "float3" -0.24348146 -0.86613166 0.079111859 ;
	setAttr ".tk[21]" -type "float3" -0.20711781 -0.86613166 0.15047976 ;
	setAttr ".tk[22]" -type "float3" -0.15047982 -0.86613166 0.20711774 ;
	setAttr ".tk[23]" -type "float3" -0.079111978 -0.86613166 0.2434814 ;
	setAttr ".tk[24]" -type "float3" -3.0518947e-008 -0.86613166 0.25601155 ;
	setAttr ".tk[25]" -type "float3" 0.079111889 -0.86613166 0.24348138 ;
	setAttr ".tk[26]" -type "float3" 0.15047976 -0.86613166 0.20711772 ;
	setAttr ".tk[27]" -type "float3" 0.20711772 -0.86613166 0.15047975 ;
	setAttr ".tk[28]" -type "float3" 0.24348138 -0.86613166 0.079111837 ;
	setAttr ".tk[29]" -type "float3" 0.25601149 -0.86613166 -4.5778425e-008 ;
	setAttr ".tk[30]" -type "float3" 0.24348138 -0.86613166 -0.079111971 ;
	setAttr ".tk[31]" -type "float3" 0.20711772 -0.86613166 -0.15047978 ;
	setAttr ".tk[32]" -type "float3" 0.15047975 -0.86613166 -0.20711774 ;
	setAttr ".tk[33]" -type "float3" 0.079111844 -0.86613166 -0.2434814 ;
	setAttr ".tk[34]" -type "float3" -2.2889212e-008 -0.86613166 -0.25601155 ;
	setAttr ".tk[35]" -type "float3" -0.079111949 -0.86613166 -0.24348138 ;
	setAttr ".tk[36]" -type "float3" -0.15047976 -0.86613166 -0.20711774 ;
	setAttr ".tk[37]" -type "float3" -0.20711772 -0.86613166 -0.15047978 ;
	setAttr ".tk[38]" -type "float3" -0.24348138 -0.86613166 -0.079111956 ;
	setAttr ".tk[39]" -type "float3" -0.25601149 -0.86613166 -4.5778425e-008 ;
	setAttr ".tk[41]" -type "float3" 0 -0.68992621 0 ;
	setAttr ".tk[42]" -type "float3" 8.8817842e-015 -0.74091655 8.9406967e-008 ;
	setAttr ".tk[43]" -type "float3" 0 -0.74091655 4.4703484e-008 ;
	setAttr ".tk[44]" -type "float3" -1.4901161e-008 -0.74091655 2.9802322e-008 ;
	setAttr ".tk[45]" -type "float3" -1.0430813e-007 -0.74091655 4.4703484e-008 ;
	setAttr ".tk[46]" -type "float3" -2.9802322e-008 -0.74091655 -2.9802322e-008 ;
	setAttr ".tk[47]" -type "float3" -5.9604645e-008 -0.74091655 1.7763568e-014 ;
	setAttr ".tk[48]" -type "float3" -2.9802322e-008 -0.74091655 0 ;
	setAttr ".tk[49]" -type "float3" -1.0430813e-007 -0.74091655 -1.4901161e-008 ;
	setAttr ".tk[50]" -type "float3" -2.9802322e-008 -0.74091655 -1.0430813e-007 ;
	setAttr ".tk[51]" -type "float3" -7.4505806e-009 -0.74091655 -4.4703484e-008 ;
	setAttr ".tk[52]" -type "float3" 1.0658141e-014 -0.74091655 -8.9406967e-008 ;
	setAttr ".tk[53]" -type "float3" -3.7252903e-008 -0.74091655 -4.4703484e-008 ;
	setAttr ".tk[54]" -type "float3" 4.4703484e-008 -0.74091655 1.4901161e-008 ;
	setAttr ".tk[55]" -type "float3" -5.9604645e-008 -0.74091655 -2.9802322e-008 ;
	setAttr ".tk[56]" -type "float3" 8.9406967e-008 -0.74091655 0 ;
	setAttr ".tk[57]" -type "float3" 5.9604645e-008 -0.74091655 1.7763568e-014 ;
	setAttr ".tk[58]" -type "float3" 1.4901161e-008 -0.74091655 2.2351742e-008 ;
	setAttr ".tk[59]" -type "float3" 1.0430813e-007 -0.74091655 4.4703484e-008 ;
	setAttr ".tk[60]" -type "float3" 2.9802322e-008 -0.74091655 2.9802322e-008 ;
	setAttr ".tk[61]" -type "float3" 1.4901161e-008 -0.74091655 4.4703484e-008 ;
createNode polySplit -n "polySplit1";
	setAttr -s 21 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 44;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[1].f" 45;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[2].f" 46;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[3].f" 47;
	setAttr ".sps[0].sp[3].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[4].f" 48;
	setAttr ".sps[0].sp[4].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[5].f" 49;
	setAttr ".sps[0].sp[5].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[6].f" 50;
	setAttr ".sps[0].sp[6].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[7].f" 51;
	setAttr ".sps[0].sp[7].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[8].f" 52;
	setAttr ".sps[0].sp[8].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[9].f" 53;
	setAttr ".sps[0].sp[9].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[10].f" 54;
	setAttr ".sps[0].sp[10].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[11].f" 55;
	setAttr ".sps[0].sp[11].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[12].f" 56;
	setAttr ".sps[0].sp[12].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[13].f" 57;
	setAttr ".sps[0].sp[13].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[14].f" 58;
	setAttr ".sps[0].sp[14].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[15].f" 59;
	setAttr ".sps[0].sp[15].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[16].f" 40;
	setAttr ".sps[0].sp[16].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[17].f" 41;
	setAttr ".sps[0].sp[17].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[18].f" 42;
	setAttr ".sps[0].sp[18].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[19].f" 43;
	setAttr ".sps[0].sp[19].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[20].f" 44;
	setAttr ".sps[0].sp[20].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".c2v" yes;
createNode polyTweak -n "polyTweak2";
	setAttr ".uopa" yes;
	setAttr -s 21 ".tk";
	setAttr ".tk[0]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[1]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[2]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[3]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[4]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[5]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[6]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[7]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[8]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[9]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[10]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[11]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[12]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[13]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[14]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[15]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[16]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[17]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[18]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[19]" -type "float3" 0 0.38762471 0 ;
	setAttr ".tk[40]" -type "float3" 0 0.34786972 0 ;
createNode polyExtrudeEdge -n "polyExtrudeEdge1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[200:219]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 0.22197106 -8.9406967e-008 ;
	setAttr ".rs" 46898;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.37199437618255615 0.2219710648059845 -0.37199446558952332 ;
	setAttr ".cbx" -type "double3" 0.3719942569732666 0.2219710648059845 0.37199428677558899 ;
createNode polyTweak -n "polyTweak3";
	setAttr ".uopa" yes;
	setAttr -s 24 ".tk";
	setAttr ".tk[102]" -type "float3" 0.037409354 0.43879214 0.11513415 ;
	setAttr ".tk[103]" -type "float3" 0.071156844 0.43879214 0.097938962 ;
	setAttr ".tk[104]" -type "float3" 0.097938962 0.43879214 0.071156815 ;
	setAttr ".tk[105]" -type "float3" 0.11513414 0.43879214 0.037409343 ;
	setAttr ".tk[106]" -type "float3" 0.12105929 0.43879214 -2.1647075e-008 ;
	setAttr ".tk[107]" -type "float3" 0.11513414 0.43879214 -0.037409391 ;
	setAttr ".tk[108]" -type "float3" 0.097938955 0.43879214 -0.071156867 ;
	setAttr ".tk[109]" -type "float3" 0.071156815 0.43879214 -0.097938977 ;
	setAttr ".tk[110]" -type "float3" 0.03740935 0.43879214 -0.11513416 ;
	setAttr ".tk[111]" -type "float3" -1.0823538e-008 0.43879214 -0.12105929 ;
	setAttr ".tk[112]" -type "float3" -0.037409354 0.43879214 -0.11513415 ;
	setAttr ".tk[113]" -type "float3" -0.071156844 0.43879214 -0.09793897 ;
	setAttr ".tk[114]" -type "float3" -0.097938962 0.43879214 -0.071156859 ;
	setAttr ".tk[115]" -type "float3" -0.11513414 0.43879214 -0.037409361 ;
	setAttr ".tk[116]" -type "float3" -0.12105929 0.43879214 -2.1647075e-008 ;
	setAttr ".tk[117]" -type "float3" -0.11513421 0.43879214 0.037409354 ;
	setAttr ".tk[118]" -type "float3" -0.097939014 0.43879214 0.071156859 ;
	setAttr ".tk[119]" -type "float3" -0.071156889 0.43879214 0.097938977 ;
	setAttr ".tk[120]" -type "float3" -0.037409399 0.43879214 0.11513416 ;
	setAttr ".tk[121]" -type "float3" -1.4431389e-008 0.43879214 0.12105929 ;
createNode deleteComponent -n "deleteComponent1";
	setAttr ".dc" -type "componentList" 1 "f[60:79]";
createNode deleteComponent -n "deleteComponent2";
	setAttr ".dc" -type "componentList" 1 "f[100:119]";
createNode polyExtrudeEdge -n "polyExtrudeEdge2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[180:199]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 0.22197106 -8.9406967e-008 ;
	setAttr ".rs" 54418;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.37199437618255615 0.2219710648059845 -0.37199446558952332 ;
	setAttr ".cbx" -type "double3" 0.3719942569732666 0.2219710648059845 0.37199428677558899 ;
createNode polyExtrudeEdge -n "polyExtrudeEdge3";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[202]" "e[204]" "e[206]" "e[208]" "e[210]" "e[212]" "e[214]" "e[216]" "e[218]" "e[220]" "e[222]" "e[224]" "e[226]" "e[228]" "e[230]" "e[232]" "e[234]" "e[236]" "e[238:239]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 0.22197106 -8.9406967e-008 ;
	setAttr ".rs" 52149;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.20649644732475281 0.2219710648059845 -0.20649650692939758 ;
	setAttr ".cbx" -type "double3" 0.20649632811546326 0.2219710648059845 0.20649632811546326 ;
createNode polyTweak -n "polyTweak4";
	setAttr ".uopa" yes;
	setAttr -s 21 ".tk";
	setAttr ".tk[101]" -type "float3" 0.051141612 0 0.15739788 ;
	setAttr ".tk[102]" -type "float3" 0.097277254 0 0.13389054 ;
	setAttr ".tk[103]" -type "float3" 0.13389054 0 0.097277246 ;
	setAttr ".tk[104]" -type "float3" 0.15739788 0 0.051141594 ;
	setAttr ".tk[105]" -type "float3" 0.16549793 0 -2.9593323e-008 ;
	setAttr ".tk[106]" -type "float3" 0.15739788 0 -0.051141653 ;
	setAttr ".tk[107]" -type "float3" 0.13389052 0 -0.097277284 ;
	setAttr ".tk[108]" -type "float3" 0.097277246 0 -0.13389055 ;
	setAttr ".tk[109]" -type "float3" 0.051141601 0 -0.1573979 ;
	setAttr ".tk[110]" -type "float3" -1.4796663e-008 0 -0.16549796 ;
	setAttr ".tk[111]" -type "float3" -0.051141612 0 -0.15739788 ;
	setAttr ".tk[112]" -type "float3" -0.097277254 0 -0.13389054 ;
	setAttr ".tk[113]" -type "float3" -0.13389054 0 -0.097277284 ;
	setAttr ".tk[114]" -type "float3" -0.15739788 0 -0.051141638 ;
	setAttr ".tk[115]" -type "float3" -0.16549793 0 -2.9593323e-008 ;
	setAttr ".tk[116]" -type "float3" -0.15739796 0 0.051141612 ;
	setAttr ".tk[117]" -type "float3" -0.13389066 0 0.097277276 ;
	setAttr ".tk[118]" -type "float3" -0.097277299 0 0.13389055 ;
	setAttr ".tk[119]" -type "float3" -0.051141653 0 0.1573979 ;
	setAttr ".tk[120]" -type "float3" -1.9728889e-008 0 0.16549796 ;
createNode polyExtrudeEdge -n "polyExtrudeEdge4";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[242]" "e[244]" "e[246]" "e[248]" "e[250]" "e[252]" "e[254]" "e[256]" "e[258]" "e[260]" "e[262]" "e[264]" "e[266]" "e[268]" "e[270]" "e[272]" "e[274]" "e[276]" "e[278:279]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 0.32065719 -8.9406967e-008 ;
	setAttr ".rs" 57263;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.20649644732475281 0.32065719366073608 -0.20649650692939758 ;
	setAttr ".cbx" -type "double3" 0.20649632811546326 0.32065719366073608 0.20649632811546326 ;
createNode polyTweak -n "polyTweak5";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[121:140]" -type "float3"  0 0.098686129 0 0 0.098686129
		 0 0 0.098686129 0 0 0.098686129 0 0 0.098686129 0 0 0.098686129 0 0 0.098686129 0
		 0 0.098686129 0 0 0.098686129 0 0 0.098686129 0 0 0.098686129 0 0 0.098686129 0 0
		 0.098686129 0 0 0.098686129 0 0 0.098686129 0 0 0.098686129 0 0 0.098686129 0 0 0.098686129
		 0 0 0.098686129 0 0 0.098686129 0;
createNode polyExtrudeEdge -n "polyExtrudeEdge5";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[282]" "e[284]" "e[286]" "e[288]" "e[290]" "e[292]" "e[294]" "e[296]" "e[298]" "e[300]" "e[302]" "e[304]" "e[306]" "e[308]" "e[310]" "e[312]" "e[314]" "e[316]" "e[318:319]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 0.32065719 -8.9406967e-008 ;
	setAttr ".rs" 48786;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.078570634126663208 0.32065719366073608 -0.07857067883014679 ;
	setAttr ".cbx" -type "double3" 0.078570514917373657 0.32065719366073608 0.078570500016212463 ;
createNode polyTweak -n "polyTweak6";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[141:160]" -type "float3"  0.039531291 0 0.12166481 0.075192928
		 0 0.1034942 0.1034942 0 0.075192928 0.12166478 0 0.039531264 0.12792581 0 -2.287492e-008
		 0.12166478 0 -0.039531313 0.1034942 0 -0.075192951 0.075192928 0 -0.10349423 0.039531276
		 0 -0.12166482 -1.143746e-008 0 -0.12792583 -0.039531291 0 -0.12166481 -0.075192928
		 0 -0.10349422 -0.1034942 0 -0.075192936 -0.12166478 0 -0.039531302 -0.12792581 0
		 -2.287492e-008 -0.12166485 0 0.039531291 -0.10349427 0 0.075192936 -0.075192988 0
		 0.10349423 -0.039531313 0 0.12166482 -1.5249944e-008 0 0.12792583;
createNode polyExtrudeEdge -n "polyExtrudeEdge6";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[322]" "e[324]" "e[326]" "e[328]" "e[330]" "e[332]" "e[334]" "e[336]" "e[338]" "e[340]" "e[342]" "e[344]" "e[346]" "e[348]" "e[350]" "e[352]" "e[354]" "e[356]" "e[358:359]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 0.34608904 -8.9406967e-008 ;
	setAttr ".rs" 53619;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.078570634126663208 0.34608903527259827 -0.07857067883014679 ;
	setAttr ".cbx" -type "double3" 0.078570514917373657 0.34608903527259827 0.078570500016212463 ;
createNode polyTweak -n "polyTweak7";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[161:180]" -type "float3"  0 0.025431845 0 0 0.025431845
		 0 0 0.025431845 0 0 0.025431845 0 0 0.025431845 0 0 0.025431845 0 0 0.025431845 0
		 0 0.025431845 0 0 0.025431845 0 0 0.025431845 0 0 0.025431845 0 0 0.025431845 0 0
		 0.025431845 0 0 0.025431845 0 0 0.025431845 0 0 0.025431845 0 0 0.025431845 0 0 0.025431845
		 0 0 0.025431845 0 0 0.025431845 0;
createNode polyExtrudeEdge -n "polyExtrudeEdge7";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[362]" "e[364]" "e[366]" "e[368]" "e[370]" "e[372]" "e[374]" "e[376]" "e[378]" "e[380]" "e[382]" "e[384]" "e[386]" "e[388]" "e[390]" "e[392]" "e[394]" "e[396]" "e[398:399]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 -0.67354834 -8.9406967e-008 ;
	setAttr ".rs" 60424;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.078570634126663208 -0.67354831152423666 -0.07857067883014679 ;
	setAttr ".cbx" -type "double3" 0.078570514917373657 -0.67354831152423666 0.078570500016212463 ;
createNode polyTweak -n "polyTweak8";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[181:200]" -type "float3"  0 0.63857329 0 0 0.63857329
		 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329
		 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329
		 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329 0 0 0.63857329
		 0;
createNode polyExtrudeEdge -n "polyExtrudeEdge8";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[402]" "e[404]" "e[406]" "e[408]" "e[410]" "e[412]" "e[414]" "e[416]" "e[418]" "e[420]" "e[422]" "e[424]" "e[426]" "e[428]" "e[430]" "e[432]" "e[434]" "e[436]" "e[438:439]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 -0.67354834 -8.9406967e-008 ;
	setAttr ".rs" 65398;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.13678717613220215 -0.67354831152423666 -0.13678723573684692 ;
	setAttr ".cbx" -type "double3" 0.1367870569229126 -0.67354831152423666 0.1367870569229126 ;
createNode polyTweak -n "polyTweak9";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[201:220]" -type "float3"  -0.01798992 0 -0.055367179
		 -0.034218814 0 -0.047098178 -0.047098178 0 -0.034218788 -0.055367142 0 -0.017989917
		 -0.058216549 0 1.0409926e-008 -0.055367142 0 0.01798992 -0.047098171 0 0.034218829
		 -0.034218788 0 0.047098231 -0.01798992 0 0.055367179 5.2049631e-009 0 0.058216561
		 0.01798992 0 0.055367179 0.034218814 0 0.047098212 0.047098178 0 0.034218818 0.055367142
		 0 0.017989919 0.058216549 0 1.0409926e-008 0.055367231 0 -0.01798992 0.047098231
		 0 -0.034218814 0.034218848 0 -0.047098231 0.01798992 0 -0.055367179 6.9399473e-009
		 0 -0.058216561;
createNode polyExtrudeEdge -n "polyExtrudeEdge9";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[442]" "e[444]" "e[446]" "e[448]" "e[450]" "e[452]" "e[454]" "e[456]" "e[458]" "e[460]" "e[462]" "e[464]" "e[466]" "e[468]" "e[470]" "e[472]" "e[474]" "e[476]" "e[478:479]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 -0.5381881 -8.9406967e-008 ;
	setAttr ".rs" 46289;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.27068519592285156 -0.53818807058795737 -0.27068525552749634 ;
	setAttr ".cbx" -type "double3" 0.27068507671356201 -0.53818807058795737 0.27068507671356201 ;
createNode polyTweak -n "polyTweak10";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[221:240]" -type "float3"  -0.041376811 0.13536029 -0.12734452
		 -0.078703277 0.13536029 -0.10832578 -0.10832578 0.13536029 -0.07870321 -0.12734443
		 0.13536029 -0.041376807 -0.13389803 0.13536029 2.394283e-008 -0.12734443 0.13536029
		 0.041376811 -0.10832577 0.13536029 0.078703284 -0.07870321 0.13536029 0.10832594
		 -0.041376811 0.13536029 0.12734452 1.1971415e-008 0.13536029 0.13389803 0.041376811
		 0.13536029 0.12734452 0.078703277 0.13536029 0.10832586 0.10832578 0.13536029 0.078703277
		 0.12734443 0.13536029 0.041376811 0.13389803 0.13536029 2.394283e-008 0.12734459
		 0.13536029 -0.041376811 0.10832594 0.13536029 -0.078703277 0.078703374 0.13536029
		 -0.10832594 0.041376811 0.13536029 -0.12734452 1.5961881e-008 0.13536029 -0.13389803;
createNode polyExtrudeEdge -n "polyExtrudeEdge10";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[482]" "e[484]" "e[486]" "e[488]" "e[490]" "e[492]" "e[494]" "e[496]" "e[498]" "e[500]" "e[502]" "e[504]" "e[506]" "e[508]" "e[510]" "e[512]" "e[514]" "e[516]" "e[518:519]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 -0.36678323 -8.9406967e-008 ;
	setAttr ".rs" 63639;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.6037711501121521 -0.36678323202594565 -0.6037711501121521 ;
	setAttr ".cbx" -type "double3" 0.60377103090286255 -0.36678323202594565 0.60377097129821777 ;
createNode polyTweak -n "polyTweak11";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[241:260]" -type "float3"  -0.10292929 0.17140478 -0.31678349
		 -0.19578299 0.17140478 -0.26947206 -0.26947206 0.17140478 -0.19578287 -0.31678283
		 0.17140478 -0.10292926 -0.33308595 0.17140478 5.9560307e-008 -0.31678283 0.17140478
		 0.10292929 -0.26947206 0.17140478 0.19578305 -0.19578287 0.17140478 0.26947248 -0.10292929
		 0.17140478 0.31678349 2.9780153e-008 0.17140478 0.33308589 0.10292929 0.17140478
		 0.31678349 0.19578299 0.17140478 0.2694723 0.26947206 0.17140478 0.19578299 0.31678283
		 0.17140478 0.10292929 0.33308595 0.17140478 5.9560307e-008 0.31678373 0.17140478
		 -0.10292929 0.26947248 0.17140478 -0.19578299 0.19578296 0.17140478 -0.26947248 0.10292929
		 0.17140478 -0.31678349 3.9706894e-008 0.17140478 -0.33308589;
createNode polyExtrudeEdge -n "polyExtrudeEdge11";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[522]" "e[524]" "e[526]" "e[528]" "e[530]" "e[532]" "e[534]" "e[536]" "e[538]" "e[540]" "e[542]" "e[544]" "e[546]" "e[548]" "e[550]" "e[552]" "e[554]" "e[556]" "e[558:559]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -5.9604645e-008 -0.23308179 -5.9604645e-008 ;
	setAttr ".rs" 37196;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.88475495576858521 -0.23308178835376547 -0.88475477695465088 ;
	setAttr ".cbx" -type "double3" 0.88475483655929565 -0.23308178835376547 0.88475465774536133 ;
createNode polyTweak -n "polyTweak12";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[261:280]" -type "float3"  -0.086828858 0.13370147 -0.26723132
		 -0.16515803 0.13370147 -0.22732067 -0.22732067 0.13370147 -0.16515793 -0.26723099
		 0.13370147 -0.086828858 -0.28098381 0.13370147 5.0243759e-008 -0.26723099 0.13370147
		 0.086828858 -0.22732067 0.13370147 0.16515806 -0.16515793 0.13370147 0.22732083 -0.086828858
		 0.13370147 0.26723129 2.512188e-008 0.13370147 0.28098366 0.086828858 0.13370147
		 0.26723129 0.16515803 0.13370147 0.22732078 0.22732067 0.13370147 0.16515802 0.26723099
		 0.13370147 0.086828858 0.28098381 0.13370147 5.0243759e-008 0.26723146 0.13370147
		 -0.086828858 0.22732083 0.13370147 -0.16515802 0.16515815 0.13370147 -0.22732083
		 0.086828858 0.13370147 -0.26723132 3.3495848e-008 0.13370147 -0.28098366;
createNode polyExtrudeEdge -n "polyExtrudeEdge12";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[562]" "e[564]" "e[566]" "e[568]" "e[570]" "e[572]" "e[574]" "e[576]" "e[578]" "e[580]" "e[582]" "e[584]" "e[586]" "e[588]" "e[590]" "e[592]" "e[594]" "e[596]" "e[598:599]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 0.55591738 -5.9604645e-008 ;
	setAttr ".rs" 48736;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.2981319427490234 0.55591741151348306 -1.2981318235397339 ;
	setAttr ".cbx" -type "double3" 1.2981319427490234 0.55591741151348306 1.2981317043304443 ;
createNode polyTweak -n "polyTweak13";
	setAttr ".uopa" yes;
	setAttr -s 100 ".tk[201:300]" -type "float3"  -0.011685155 0.43090543 -0.035963174
		 -0.022226473 0.43090543 -0.030592123 -0.030592116 0.43090543 -0.022226466 -0.03596314
		 0.43090543 -0.011685157 -0.037813921 0.43090543 -1.4770053e-009 -0.03596314 0.43090543
		 0.011685146 -0.030592114 0.43090543 0.022226473 -0.022226453 0.43090543 0.030592129
		 -0.011685155 0.43090543 0.035963144 3.3808276e-009 0.43090543 0.037813921 0.011685155
		 0.43090543 0.035963144 0.022226473 0.43090543 0.030592127 0.030592116 0.43090543
		 0.02222647 0.03596314 0.43090543 0.011685144 0.037813921 0.43090543 -1.4770053e-009
		 0.035963193 0.43090543 -0.011685173 0.030592132 0.43090543 -0.022226477 0.022226481
		 0.43090543 -0.030592171 0.011685156 0.43090543 -0.035963174 4.5077697e-009 0.43090543
		 -0.037813924 -0.023123523 0.46832493 -0.071166754 -0.043983482 0.46832493 -0.060538076
		 -0.060538068 0.46832493 -0.043983474 -0.071166687 0.46832493 -0.023123525 -0.074829176
		 0.46832493 5.1418394e-009 -0.071166687 0.46832493 0.023123518 -0.060538068 0.46832493
		 0.043983486 -0.043983467 0.46832493 0.060538184 -0.023123523 0.46832493 0.071166746
		 6.6902497e-009 0.46832493 0.074829176 0.023123523 0.46832493 0.071166746 0.043983482
		 0.46832493 0.060538154 0.060538068 0.46832493 0.043983478 0.071166687 0.46832493
		 0.023123518 0.074829176 0.46832493 5.1418394e-009 0.071166791 0.46832493 -0.023123525
		 0.060538188 0.46832493 -0.043983486 0.043983508 0.46832493 -0.060538203 0.023123523
		 0.46832493 -0.071166754 8.9203303e-009 0.46832493 -0.074829198 -0.05157768 0.51570863
		 -0.15873949 -0.098106422 0.51570863 -0.13503194 -0.13503189 0.51570863 -0.098106407
		 -0.15873922 0.51570863 -0.051577672 -0.16690862 0.51570863 2.1606906e-008 -0.15873922
		 0.51570863 0.051577669 -0.13503189 0.51570863 0.098106429 -0.098106399 0.51570863
		 0.13503203 -0.05157768 0.51570863 0.15873948 1.4922778e-008 0.51570863 0.16690859
		 0.05157768 0.51570863 0.15873948 0.098106422 0.51570863 0.13503198 0.13503189 0.51570863
		 0.098106422 0.15873922 0.51570863 0.051577669 0.16690862 0.51570863 2.1606906e-008
		 0.15873967 0.51570863 -0.051577691 0.13503204 0.51570863 -0.098106429 0.098106436
		 0.51570863 -0.13503204 0.05157768 0.51570863 -0.15873949 1.9897039e-008 0.51570863
		 -0.16690862 -0.075580955 0.55266953 -0.23261392 -0.14376336 0.55266953 -0.19787329
		 -0.19787329 0.55266953 -0.14376332 -0.23261365 0.55266953 -0.075580955 -0.24458475
		 0.55266953 3.5496463e-008 -0.23261365 0.55266953 0.075580955 -0.19787329 0.55266953
		 0.14376338 -0.14376332 0.55266953 0.19787349 -0.075580955 0.55266953 0.23261391 2.1867573e-008
		 0.55266953 0.24458474 0.075580955 0.55266953 0.23261391 0.14376336 0.55266953 0.19787344
		 0.19787329 0.55266953 0.14376336 0.23261365 0.55266953 0.075580955 0.24458475 0.55266953
		 3.5496463e-008 0.23261398 0.55266953 -0.075580962 0.1978735 0.55266953 -0.14376338
		 0.14376341 0.55266953 -0.1978735 0.075580955 0.55266953 -0.23261392 2.9156764e-008
		 0.55266953 -0.24458474 -0.12774074 0.7889992 -0.39314535 -0.24297693 0.7889992 -0.33442944
		 -0.33442944 0.7889992 -0.24297689 -0.39314428 0.7889992 -0.12774074 -0.41337705 0.7889992
		 5.9993276e-008 -0.39314428 0.7889992 0.12774074 -0.33442944 0.7889992 0.24297695
		 -0.24297689 0.7889992 0.33442962 -0.12774074 0.7889992 0.39314467 3.6958799e-008
		 0.7889992 0.41337702 0.12774074 0.7889992 0.39314467 0.24297693 0.7889992 0.33442956
		 0.33442944 0.7889992 0.24297693 0.39314428 0.7889992 0.12774074 0.41337705 0.7889992
		 5.9993276e-008 0.39314544 0.7889992 -0.12774076 0.33442965 0.7889992 -0.24297693
		 0.24297698 0.7889992 -0.33442965 0.12774074 0.7889992 -0.39314535 4.927837e-008 0.7889992
		 -0.41337702;
createNode polyExtrudeEdge -n "polyExtrudeEdge13";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[602]" "e[604]" "e[606]" "e[608]" "e[610]" "e[612]" "e[614]" "e[616]" "e[618]" "e[620]" "e[622]" "e[624]" "e[626]" "e[628]" "e[630]" "e[632]" "e[634]" "e[636]" "e[638:639]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 1.2173761 0 ;
	setAttr ".rs" 56390;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.6203973293304443 1.2173761422111149 -1.6203970909118652 ;
	setAttr ".cbx" -type "double3" 1.6203973293304443 1.2173761422111149 1.6203970909118652 ;
createNode polyTweak -n "polyTweak14";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[301:320]" -type "float3"  -0.099585429 0.66145867 -0.30649281
		 -0.18942273 0.66145867 -0.26071817 -0.26071823 0.66145867 -0.18942252 -0.30649227
		 0.66145867 -0.099585414 -0.32226539 0.66145867 4.6770147e-008 -0.30649227 0.66145867
		 0.099585414 -0.26071823 0.66145867 0.18942273 -0.18942261 0.66145867 0.26071852 -0.099585429
		 0.66145867 0.30649263 1.4015725e-008 0.66145867 0.32226533 0.099585362 0.66145867
		 0.30649263 0.18942267 0.66145867 0.26071841 0.26071805 0.66145867 0.18942273 0.30649215
		 0.66145867 0.099585414 0.32226539 0.66145867 4.6770147e-008 0.30649233 0.66145867
		 -0.099585421 0.26071852 0.66145867 -0.18942273 0.1894227 0.66145867 -0.26071858 0.099585362
		 0.66145867 -0.30649281 2.3619929e-008 0.66145867 -0.32226533;
createNode polyExtrudeEdge -n "polyExtrudeEdge14";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[642]" "e[644]" "e[646]" "e[648]" "e[650]" "e[652]" "e[654]" "e[656]" "e[658]" "e[660]" "e[662]" "e[664]" "e[666]" "e[668]" "e[670]" "e[672]" "e[674]" "e[676]" "e[678:679]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 1.435213 0 ;
	setAttr ".rs" 41594;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.6370613574981689 1.4352129990531559 -1.6370611190795898 ;
	setAttr ".cbx" -type "double3" 1.6370613574981689 1.4352129990531559 1.6370611190795898 ;
createNode polyTweak -n "polyTweak15";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[321:340]" -type "float3"  -0.005149466 0.2178369 -0.015848422
		 -0.0097948583 0.2178369 -0.013481462 -0.01348147 0.2178369 -0.0097948555 -0.015848389
		 0.2178369 -0.005149466 -0.016664013 0.2178369 1.8054709e-009 -0.015848389 0.2178369
		 0.0051494641 -0.01348147 0.2178369 0.0097948555 -0.009794849 0.2178369 0.013481485
		 -0.005149466 0.2178369 0.0158484 7.2473794e-010 0.2178369 0.016664008 0.0051494613
		 0.2178369 0.0158484 0.0097948555 0.2178369 0.013481484 0.013481468 0.2178369 0.0097948592
		 0.015848387 0.2178369 0.0051494641 0.016664013 0.2178369 1.8054709e-009 0.015848422
		 0.2178369 -0.005149466 0.013481488 0.2178369 -0.0097948592 0.0097948574 0.2178369
		 -0.01348149 0.0051494613 0.2178369 -0.015848422 1.221363e-009 0.2178369 -0.016664008;
createNode polyExtrudeEdge -n "polyExtrudeEdge15";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[682]" "e[684]" "e[686]" "e[688]" "e[690]" "e[692]" "e[694]" "e[696]" "e[698]" "e[700]" "e[702]" "e[704]" "e[706]" "e[708]" "e[710]" "e[712]" "e[714]" "e[716]" "e[718:719]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 1.4729618 0 ;
	setAttr ".rs" 42934;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.1511328220367432 1.4729618126823063 -1.1511325836181641 ;
	setAttr ".cbx" -type "double3" 1.1511328220367432 1.4729618126823063 1.1511325836181641 ;
createNode polyTweak -n "polyTweak16";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[341:360]" -type "float3"  0.15016034 0.037748706 0.46214584
		 0.28562158 0.037748706 0.39312476 0.39312479 0.037748706 0.28562152 0.46214482 0.037748706
		 0.15016034 0.48592854 0.037748706 -5.2648204e-008 0.46214482 0.037748706 -0.15016033
		 0.39312479 0.037748706 -0.28562158 0.28562152 0.037748706 -0.39312506 0.15016034
		 0.037748706 -0.46214542 -2.113363e-008 0.037748706 -0.48592854 -0.1501603 0.037748706
		 -0.46214542 -0.28562152 0.037748706 -0.39312488 -0.39312458 0.037748706 -0.28562155
		 -0.46214467 0.037748706 -0.15016033 -0.48592854 0.037748706 -5.2648204e-008 -0.46214584
		 0.037748706 0.15016037 -0.39312509 0.037748706 0.28562158 -0.28562167 0.037748706
		 0.39312509 -0.1501603 0.037748706 0.46214584 -3.5615411e-008 0.037748706 0.48592854;
createNode polyExtrudeEdge -n "polyExtrudeEdge16";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[722]" "e[724]" "e[726]" "e[728]" "e[730]" "e[732]" "e[734]" "e[736]" "e[738]" "e[740]" "e[742]" "e[744]" "e[746]" "e[748]" "e[750]" "e[752]" "e[754]" "e[756]" "e[758:759]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 1.6075355 0 ;
	setAttr ".rs" 45780;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.1511328220367432 1.6075355107261295 -1.1511325836181641 ;
	setAttr ".cbx" -type "double3" 1.1511328220367432 1.6075355107261295 1.1511325836181641 ;
createNode polyTweak -n "polyTweak17";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[361:380]" -type "float3"  0 0.13457377 0 0 0.13457377
		 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377
		 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377
		 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377 0 0 0.13457377
		 0;
createNode polyExtrudeEdge -n "polyExtrudeEdge17";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[762]" "e[764]" "e[766]" "e[768]" "e[770]" "e[772]" "e[774]" "e[776]" "e[778]" "e[780]" "e[782]" "e[784]" "e[786]" "e[788]" "e[790]" "e[792]" "e[794]" "e[796]" "e[798:799]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 1.6875154 0 ;
	setAttr ".rs" 56349;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.6647614240646362 1.6875154072715397 -1.6647610664367676 ;
	setAttr ".cbx" -type "double3" 1.6647614240646362 1.6875154072715397 1.6647610664367676 ;
createNode polyTweak -n "polyTweak18";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[381:400]" -type "float3"  -0.15872012 0.079979956 -0.48848987
		 -0.30190328 0.079979956 -0.41553447 -0.41553447 0.079979956 -0.30190304 -0.48848903
		 0.079979956 -0.15872012 -0.5136286 0.079979956 5.564933e-008 -0.48848903 0.079979956
		 0.15872011 -0.41553447 0.079979956 0.30190328 -0.30190304 0.079979956 0.41553465
		 -0.15872012 0.079979956 0.48848966 2.2338337e-008 0.079979956 0.51362854 0.15872009
		 0.079979956 0.48848966 0.30190313 0.079979956 0.41553459 0.41553444 0.079979956 0.30190328
		 0.48848903 0.079979956 0.15872011 0.5136286 0.079979956 5.564933e-008 0.48848987
		 0.079979956 -0.15872012 0.4155347 0.079979956 -0.30190331 0.30190331 0.079979956
		 -0.41553479 0.15872009 0.079979956 -0.48848987 3.7645631e-008 0.079979956 -0.51362854;
createNode polyExtrudeEdge -n "polyExtrudeEdge18";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[802]" "e[804]" "e[806]" "e[808]" "e[810]" "e[812]" "e[814]" "e[816]" "e[818]" "e[820]" "e[822]" "e[824]" "e[826]" "e[828]" "e[830]" "e[832]" "e[834]" "e[836]" "e[838:839]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 1.8190216 0 ;
	setAttr ".rs" 56262;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.3231182098388672 1.8190216118766422 -1.3231179714202881 ;
	setAttr ".cbx" -type "double3" 1.3231182098388672 1.8190216118766422 1.3231179714202881 ;
createNode polyTweak -n "polyTweak19";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[401:420]" -type "float3"  0.1055737 0.13150629 0.32492208
		 0.20081277 0.13150629 0.27639514 0.27639514 0.13150629 0.20081267 0.32492134 0.13150629
		 0.1055737 0.34164315 0.13150629 -3.7015496e-008 0.32492134 0.13150629 -0.10557368
		 0.27639514 0.13150629 -0.20081277 0.20081268 0.13150629 -0.27639535 0.1055737 0.13150629
		 -0.32492194 -1.4858485e-008 0.13150629 -0.34164315 -0.10557359 0.13150629 -0.32492194
		 -0.20081273 0.13150629 -0.27639523 -0.27639517 0.13150629 -0.20081276 -0.32492134
		 0.13150629 -0.10557368 -0.34164315 0.13150629 -3.7015496e-008 -0.32492208 0.13150629
		 0.1055737 -0.27639541 0.13150629 0.20081282 -0.20081288 0.13150629 0.27639544 -0.10557359
		 0.13150629 0.32492208 -2.5040212e-008 0.13150629 0.34164315;
createNode polyExtrudeEdge -n "polyExtrudeEdge19";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[842]" "e[844]" "e[846]" "e[848]" "e[850]" "e[852]" "e[854]" "e[856]" "e[858]" "e[860]" "e[862]" "e[864]" "e[866]" "e[868]" "e[870]" "e[872]" "e[874]" "e[876]" "e[878:879]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 1.9582304 0 ;
	setAttr ".rs" 62541;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.1949272155761719 1.958230405516779 -1.1949270963668823 ;
	setAttr ".cbx" -type "double3" 1.1949272155761719 1.958230405516779 1.1949270963668823 ;
createNode polyTweak -n "polyTweak20";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[421:440]" -type "float3"  0.039613225 0.13920875 0.12191685
		 0.075348742 0.13920875 0.10370871 0.10370872 0.13920875 0.075348653 0.12191663 0.13920875
		 0.039613225 0.12819101 0.13920875 -1.388891e-008 0.12191663 0.13920875 -0.039613232
		 0.10370872 0.13920875 -0.075348742 0.07534866 0.13920875 -0.10370879 0.039613225
		 0.13920875 -0.12191678 -5.5751839e-009 0.13920875 -0.12819092 -0.039613221 0.13920875
		 -0.12191678 -0.07534872 0.13920875 -0.10370877 -0.1037087 0.13920875 -0.075348742
		 -0.12191663 0.13920875 -0.039613232 -0.12819101 0.13920875 -1.388891e-008 -0.12191685
		 0.13920875 0.039613225 -0.1037088 0.13920875 0.075348742 -0.075348742 0.13920875
		 0.1037088 -0.039613221 0.13920875 0.12191685 -9.3955643e-009 0.13920875 0.12819092;
createNode polyExtrudeEdge -n "polyExtrudeEdge20";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[882]" "e[884]" "e[886]" "e[888]" "e[890]" "e[892]" "e[894]" "e[896]" "e[898]" "e[900]" "e[902]" "e[904]" "e[906]" "e[908]" "e[910]" "e[912]" "e[914]" "e[916]" "e[918:919]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 2.0270839 0 ;
	setAttr ".rs" 46115;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.87444984912872314 2.0270837838126772 -0.87444972991943359 ;
	setAttr ".cbx" -type "double3" 0.87444984912872314 2.0270837838126772 0.87444972991943359 ;
createNode polyTweak -n "polyTweak21";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[441:460]" -type "float3"  0.09903305 0.068853371 0.30479211
		 0.18837182 0.068853371 0.25927171 0.25927177 0.068853371 0.18837164 0.30479157 0.068853371
		 0.09903305 0.3204774 0.068853371 -3.4722273e-008 0.30479157 0.068853371 -0.099033028
		 0.25927177 0.068853371 -0.18837182 0.18837166 0.068853371 -0.25927195 0.09903305
		 0.068853371 -0.30479181 -1.3937949e-008 0.068853371 -0.32047737 -0.099033013 0.068853371
		 -0.30479181 -0.18837173 0.068853371 -0.25927192 -0.25927168 0.068853371 -0.18837176
		 -0.30479157 0.068853371 -0.099033028 -0.3204774 0.068853371 -3.4722273e-008 -0.30479211
		 0.068853371 0.099033065 -0.25927198 0.068853371 0.18837182 -0.18837182 0.068853371
		 0.25927201 -0.099033013 0.068853371 0.30479211 -2.3488909e-008 0.068853371 0.32047737;
createNode polyExtrudeEdge -n "polyExtrudeEdge21";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 19 "e[922]" "e[924]" "e[926]" "e[928]" "e[930]" "e[932]" "e[934]" "e[936]" "e[938]" "e[940]" "e[942]" "e[944]" "e[946]" "e[948]" "e[950]" "e[952]" "e[954]" "e[956]" "e[958:959]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 2.1377852 0 ;
	setAttr ".rs" 62799;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.43621653318405151 2.1377851063682192 -0.43621647357940674 ;
	setAttr ".cbx" -type "double3" 0.43621653318405151 2.1377851063682192 0.43621647357940674 ;
createNode polyTweak -n "polyTweak22";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[461:480]" -type "float3"  0.13542162 0.11070096 0.41678464
		 0.25758705 0.11070096 0.35453817 0.35453829 0.11070096 0.25758687 0.41678408 0.11070096
		 0.13542162 0.43823332 0.11070096 -4.7480608e-008 0.41678408 0.11070096 -0.13542162
		 0.35453829 0.11070096 -0.25758705 0.2575869 0.11070096 -0.35453856 0.13542162 0.11070096
		 -0.41678432 -1.9059298e-008 0.11070096 -0.43823326 -0.1354216 0.11070096 -0.41678432
		 -0.25758693 0.11070096 -0.35453844 -0.35453817 0.11070096 -0.25758699 -0.41678408
		 0.11070096 -0.13542162 -0.43823332 0.11070096 -4.7480608e-008 -0.41678464 0.11070096
		 0.13542163 -0.35453874 0.11070096 0.25758705 -0.25758705 0.11070096 0.35453877 -0.1354216
		 0.11070096 0.41678464 -3.2119644e-008 0.11070096 0.43823326;
createNode polyMergeVert -n "polyMergeVert1";
	setAttr ".ics" -type "componentList" 1 "vtx[481:500]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 -1.658210605912054 0 1;
	setAttr ".am" yes;
createNode polyTweak -n "polyTweak23";
	setAttr ".uopa" yes;
	setAttr -s 20 ".tk[481:500]" -type "float3"  0.13299721 0 0.40932313 0.25297555
		 0 0.34819111 0.34819123 0 0.25297531 0.40932244 0 0.13299721 0.43038785 0 -4.6630571e-008
		 0.40932244 0 -0.13299721 0.34819123 0 -0.25297555 0.2529754 0 -0.34819144 0.13299721
		 0 -0.40932292 -1.8718083e-008 0 -0.43038779 -0.1329972 0 -0.40932292 -0.25297549
		 0 -0.34819138 -0.34819111 0 -0.25297555 -0.40932244 0 -0.13299721 -0.43038785 0 -4.6630571e-008
		 -0.40932313 0 0.13299726 -0.34819144 0 0.25297561 -0.25297561 0 0.34819147 -0.1329972
		 0 0.40932313 -3.1544616e-008 0 0.43038779;
createNode polyCube -n "polyCube1";
	setAttr ".cuv" 4;
createNode polyBevel -n "polyBevel1";
	setAttr ".ics" -type "componentList" 1 "e[8]";
	setAttr ".ix" -type "matrix" 4.7832452807467991 0 0 0 0 0.064072636393356475 0 0
		 0 0 1 0 -5.5667450874733637 -2.0025013819502675 0 1;
	setAttr ".ws" yes;
	setAttr ".oaf" yes;
	setAttr ".o" 0.5;
	setAttr ".at" 180;
	setAttr ".fn" yes;
	setAttr ".ua" 1;
	setAttr ".mv" yes;
	setAttr ".mvt" 0.0001;
	setAttr ".sa" 30;
	setAttr ".ma" 180;
createNode polySplit -n "polySplit2";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 6;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".sps[0].sp[1].f" 6;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.49999994039535522 0.50000005960464478 
		0 ;
	setAttr ".sps[0].sp[2].f" 1;
	setAttr ".sps[0].sp[2].t" 2;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0 0.50000005960464478 0.49999994039535522 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit3";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.5 0.5 0 ;
	setAttr ".sps[0].sp[2].f" 1;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".c2v" yes;
createNode polyTweak -n "polyTweak24";
	setAttr ".uopa" yes;
	setAttr -s 9 ".tk";
	setAttr ".tk[0]" -type "float3" 0.034053709 0 1.4901161e-008 ;
	setAttr ".tk[2]" -type "float3" 0.034053709 0 1.4901161e-008 ;
	setAttr ".tk[10]" -type "float3" -0.028250838 0 -0.092082761 ;
	setAttr ".tk[11]" -type "float3" -0.028250838 0 -0.092082761 ;
createNode polyBevel -n "polyBevel2";
	setAttr ".ics" -type "componentList" 2 "e[4:5]" "e[7:9]";
	setAttr ".ix" -type "matrix" 4.7832452807467991 0 0 0 0 0.064072636393356475 0 0
		 0 0 1 0 -5.5667450874733637 -2.0025013819502675 0 1;
	setAttr ".ws" yes;
	setAttr ".oaf" yes;
	setAttr ".o" 0.5;
	setAttr ".at" 180;
	setAttr ".fn" yes;
	setAttr ".ua" 1;
	setAttr ".mv" yes;
	setAttr ".mvt" 0.0001;
	setAttr ".sa" 30;
	setAttr ".ma" 180;
createNode polyTweak -n "polyTweak25";
	setAttr ".uopa" yes;
	setAttr -s 4 ".tk";
	setAttr ".tk[0]" -type "float3" 0.021406762 0 -0.054967489 ;
	setAttr ".tk[2]" -type "float3" 0.021406762 0 -0.054967489 ;
	setAttr ".tk[4]" -type "float3" 0.038739454 0 0 ;
	setAttr ".tk[5]" -type "float3" 0.038739454 0 0 ;
createNode polySplit -n "polySplit4";
	setAttr -s 11 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 5;
	setAttr ".sps[0].sp[0].t" 3;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 3;
	setAttr ".sps[0].sp[1].t" 2;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.51657342910766602 0 0.48342657089233398 ;
	setAttr ".sps[0].sp[2].f" 3;
	setAttr ".sps[0].sp[2].t" 1;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".sps[0].sp[3].f" 13;
	setAttr ".sps[0].sp[3].t" 1;
	setAttr ".sps[0].sp[3].bc" -type "double3" 0.50033670663833618 0.49966329336166382 
		0 ;
	setAttr ".sps[0].sp[4].f" 7;
	setAttr ".sps[0].sp[4].t" 1;
	setAttr ".sps[0].sp[4].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".sps[0].sp[5].f" 7;
	setAttr ".sps[0].sp[5].t" 1;
	setAttr ".sps[0].sp[5].bc" -type "double3" 0.5 0.5 0 ;
	setAttr ".sps[0].sp[6].f" 7;
	setAttr ".sps[0].sp[6].bc" -type "double3" 0.5 0.5 0 ;
	setAttr ".sps[0].sp[7].f" 11;
	setAttr ".sps[0].sp[7].bc" -type "double3" 6.6751503879913798e-008 0.50083070993423462 
		0.49916923046112061 ;
	setAttr ".sps[0].sp[8].f" 1;
	setAttr ".sps[0].sp[8].t" 1;
	setAttr ".sps[0].sp[8].bc" -type "double3" 0.49992501735687256 0.50007498264312744 
		0 ;
	setAttr ".sps[0].sp[9].f" 1;
	setAttr ".sps[0].sp[9].t" 2;
	setAttr ".sps[0].sp[9].bc" -type "double3" 0.51802301406860352 0.48197701573371887 
		-2.9802322387695313e-008 ;
	setAttr ".sps[0].sp[10].f" 1;
	setAttr ".sps[0].sp[10].t" 4;
	setAttr ".sps[0].sp[10].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polyTweak -n "polyTweak26";
	setAttr ".uopa" yes;
	setAttr -s 12 ".tk";
	setAttr ".tk[6]" -type "float3" 0 0 -0.19498286 ;
	setAttr ".tk[7]" -type "float3" 0 0 -0.19498286 ;
	setAttr ".tk[8]" -type "float3" -0.042911749 -0.16191114 -0.25088477 ;
	setAttr ".tk[9]" -type "float3" -0.042911749 -0.16191114 -0.25088477 ;
	setAttr ".tk[10]" -type "float3" 0.0052444949 0 0.015653897 ;
	setAttr ".tk[11]" -type "float3" -0.0031642069 0 -0.016196048 ;
	setAttr ".tk[12]" -type "float3" -0.0052444949 0 0.016196052 ;
	setAttr ".tk[15]" -type "float3" 0.0052444949 0 0.015653897 ;
	setAttr ".tk[16]" -type "float3" -0.0052444949 0 0.016196052 ;
	setAttr ".tk[17]" -type "float3" -0.0031642069 0 -0.016196048 ;
	setAttr ".tk[20]" -type "float3" -0.042911749 -0.16191114 -0.25088477 ;
	setAttr ".tk[21]" -type "float3" -0.042911749 -0.16191114 -0.25088477 ;
createNode polyUnite -n "polyUnite1";
	setAttr -s 4 ".ip";
	setAttr -s 4 ".im";
createNode groupId -n "groupId1";
	setAttr ".ihi" 0;
createNode groupId -n "groupId2";
	setAttr ".ihi" 0;
createNode groupId -n "groupId3";
	setAttr ".ihi" 0;
createNode groupId -n "groupId4";
	setAttr ".ihi" 0;
createNode groupId -n "groupId5";
	setAttr ".ihi" 0;
createNode groupId -n "groupId6";
	setAttr ".ihi" 0;
createNode groupId -n "groupId7";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts1";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:20]";
createNode groupId -n "groupId8";
	setAttr ".ihi" 0;
createNode groupId -n "groupId9";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts2";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:83]";
createNode polyChipOff -n "polyChipOff1";
	setAttr ".ics" -type "componentList" 1 "f[420:499]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1.0159385517748969 0 0 0 0 1 0 0 -5.1962756527426395 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -5.1962757 0 ;
	setAttr ".rs" 36684;
	setAttr ".dup" no;
createNode polySeparate -n "polySeparate1";
	setAttr ".ic" 2;
	setAttr -s 2 ".out";
createNode groupId -n "groupId10";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts3";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:499]";
createNode groupId -n "groupId11";
	setAttr ".ihi" 0;
createNode groupId -n "groupId12";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts4";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 420 "f[0]" "f[1]" "f[2]" "f[3]" "f[4]" "f[5]" "f[6]" "f[7]" "f[8]" "f[9]" "f[10]" "f[11]" "f[12]" "f[13]" "f[14]" "f[15]" "f[16]" "f[17]" "f[18]" "f[19]" "f[20]" "f[21]" "f[22]" "f[23]" "f[24]" "f[25]" "f[26]" "f[27]" "f[28]" "f[29]" "f[30]" "f[31]" "f[32]" "f[33]" "f[34]" "f[35]" "f[36]" "f[37]" "f[38]" "f[39]" "f[40]" "f[41]" "f[42]" "f[43]" "f[44]" "f[45]" "f[46]" "f[47]" "f[48]" "f[49]" "f[50]" "f[51]" "f[52]" "f[53]" "f[54]" "f[55]" "f[56]" "f[57]" "f[58]" "f[59]" "f[60]" "f[61]" "f[62]" "f[63]" "f[64]" "f[65]" "f[66]" "f[67]" "f[68]" "f[69]" "f[70]" "f[71]" "f[72]" "f[73]" "f[74]" "f[75]" "f[76]" "f[77]" "f[78]" "f[79]" "f[80]" "f[81]" "f[82]" "f[83]" "f[84]" "f[85]" "f[86]" "f[87]" "f[88]" "f[89]" "f[90]" "f[91]" "f[92]" "f[93]" "f[94]" "f[95]" "f[96]" "f[97]" "f[98]" "f[99]" "f[100]" "f[101]" "f[102]" "f[103]" "f[104]" "f[105]" "f[106]" "f[107]" "f[108]" "f[109]" "f[110]" "f[111]" "f[112]" "f[113]" "f[114]" "f[115]" "f[116]" "f[117]" "f[118]" "f[119]" "f[120]" "f[121]" "f[122]" "f[123]" "f[124]" "f[125]" "f[126]" "f[127]" "f[128]" "f[129]" "f[130]" "f[131]" "f[132]" "f[133]" "f[134]" "f[135]" "f[136]" "f[137]" "f[138]" "f[139]" "f[140]" "f[141]" "f[142]" "f[143]" "f[144]" "f[145]" "f[146]" "f[147]" "f[148]" "f[149]" "f[150]" "f[151]" "f[152]" "f[153]" "f[154]" "f[155]" "f[156]" "f[157]" "f[158]" "f[159]" "f[160]" "f[161]" "f[162]" "f[163]" "f[164]" "f[165]" "f[166]" "f[167]" "f[168]" "f[169]" "f[170]" "f[171]" "f[172]" "f[173]" "f[174]" "f[175]" "f[176]" "f[177]" "f[178]" "f[179]" "f[180]" "f[181]" "f[182]" "f[183]" "f[184]" "f[185]" "f[186]" "f[187]" "f[188]" "f[189]" "f[190]" "f[191]" "f[192]" "f[193]" "f[194]" "f[195]" "f[196]" "f[197]" "f[198]" "f[199]" "f[200]" "f[201]" "f[202]" "f[203]" "f[204]" "f[205]" "f[206]" "f[207]" "f[208]" "f[209]" "f[210]" "f[211]" "f[212]" "f[213]" "f[214]" "f[215]" "f[216]" "f[217]" "f[218]" "f[219]" "f[220]" "f[221]" "f[222]" "f[223]" "f[224]" "f[225]" "f[226]" "f[227]" "f[228]" "f[229]" "f[230]" "f[231]" "f[232]" "f[233]" "f[234]" "f[235]" "f[236]" "f[237]" "f[238]" "f[239]" "f[240]" "f[241]" "f[242]" "f[243]" "f[244]" "f[245]" "f[246]" "f[247]" "f[248]" "f[249]" "f[250]" "f[251]" "f[252]" "f[253]" "f[254]" "f[255]" "f[256]" "f[257]" "f[258]" "f[259]" "f[260]" "f[261]" "f[262]" "f[263]" "f[264]" "f[265]" "f[266]" "f[267]" "f[268]" "f[269]" "f[270]" "f[271]" "f[272]" "f[273]" "f[274]" "f[275]" "f[276]" "f[277]" "f[278]" "f[279]" "f[280]" "f[281]" "f[282]" "f[283]" "f[284]" "f[285]" "f[286]" "f[287]" "f[288]" "f[289]" "f[290]" "f[291]" "f[292]" "f[293]" "f[294]" "f[295]" "f[296]" "f[297]" "f[298]" "f[299]" "f[300]" "f[301]" "f[302]" "f[303]" "f[304]" "f[305]" "f[306]" "f[307]" "f[308]" "f[309]" "f[310]" "f[311]" "f[312]" "f[313]" "f[314]" "f[315]" "f[316]" "f[317]" "f[318]" "f[319]" "f[320]" "f[321]" "f[322]" "f[323]" "f[324]" "f[325]" "f[326]" "f[327]" "f[328]" "f[329]" "f[330]" "f[331]" "f[332]" "f[333]" "f[334]" "f[335]" "f[336]" "f[337]" "f[338]" "f[339]" "f[340]" "f[341]" "f[342]" "f[343]" "f[344]" "f[345]" "f[346]" "f[347]" "f[348]" "f[349]" "f[350]" "f[351]" "f[352]" "f[353]" "f[354]" "f[355]" "f[356]" "f[357]" "f[358]" "f[359]" "f[360]" "f[361]" "f[362]" "f[363]" "f[364]" "f[365]" "f[366]" "f[367]" "f[368]" "f[369]" "f[370]" "f[371]" "f[372]" "f[373]" "f[374]" "f[375]" "f[376]" "f[377]" "f[378]" "f[379]" "f[380]" "f[381]" "f[382]" "f[383]" "f[384]" "f[385]" "f[386]" "f[387]" "f[388]" "f[389]" "f[390]" "f[391]" "f[392]" "f[393]" "f[394]" "f[395]" "f[396]" "f[397]" "f[398]" "f[399]" "f[400]" "f[401]" "f[402]" "f[403]" "f[404]" "f[405]" "f[406]" "f[407]" "f[408]" "f[409]" "f[410]" "f[411]" "f[412]" "f[413]" "f[414]" "f[415]" "f[416]" "f[417]" "f[418]" "f[419]";
createNode groupId -n "groupId13";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts5";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 80 "f[0]" "f[1]" "f[2]" "f[3]" "f[4]" "f[5]" "f[6]" "f[7]" "f[8]" "f[9]" "f[10]" "f[11]" "f[12]" "f[13]" "f[14]" "f[15]" "f[16]" "f[17]" "f[18]" "f[19]" "f[20]" "f[21]" "f[22]" "f[23]" "f[24]" "f[25]" "f[26]" "f[27]" "f[28]" "f[29]" "f[30]" "f[31]" "f[32]" "f[33]" "f[34]" "f[35]" "f[36]" "f[37]" "f[38]" "f[39]" "f[40]" "f[41]" "f[42]" "f[43]" "f[44]" "f[45]" "f[46]" "f[47]" "f[48]" "f[49]" "f[50]" "f[51]" "f[52]" "f[53]" "f[54]" "f[55]" "f[56]" "f[57]" "f[58]" "f[59]" "f[60]" "f[61]" "f[62]" "f[63]" "f[64]" "f[65]" "f[66]" "f[67]" "f[68]" "f[69]" "f[70]" "f[71]" "f[72]" "f[73]" "f[74]" "f[75]" "f[76]" "f[77]" "f[78]" "f[79]";
createNode polySmoothFace -n "polySmoothFace1";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".suv" yes;
	setAttr ".ps" 0.10000000149011612;
	setAttr ".ro" 1;
	setAttr ".ma" yes;
	setAttr ".m08" yes;
createNode polySmoothFace -n "polySmoothFace2";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".suv" yes;
	setAttr ".ps" 0.10000000149011612;
	setAttr ".ro" 1;
	setAttr ".ma" yes;
	setAttr ".m08" yes;
createNode script -n "uiConfigurationScriptNode";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"top\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n"
		+ "                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n"
		+ "                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n"
		+ "                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n"
		+ "            -rendererName \"base_OpenGL_Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n"
		+ "            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"side\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n"
		+ "                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n"
		+ "                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n"
		+ "                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n"
		+ "            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n"
		+ "            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"front\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n"
		+ "                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n"
		+ "                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n"
		+ "                -shadows 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n"
		+ "            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n"
		+ "            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n"
		+ "        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n"
		+ "                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n"
		+ "                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n"
		+ "                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n"
		+ "            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n"
		+ "            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n"
		+ "            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n                -showShapes 0\n                -showReferenceNodes 1\n                -showReferenceMembers 1\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n"
		+ "                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n                -showContainerContents 1\n                -ignoreDagHierarchy 0\n                -expandConnections 0\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n"
		+ "                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n"
		+ "            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n"
		+ "            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"graphEditor\" -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n"
		+ "                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n"
		+ "                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n"
		+ "                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n"
		+ "                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n"
		+ "                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n"
		+ "                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dopeSheetPanel\" -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n"
		+ "                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n"
		+ "                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n"
		+ "                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n"
		+ "                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"clipEditorPanel\" -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n"
		+ "                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"sequenceEditorPanel\" -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperGraphPanel\" -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n"
		+ "                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n"
		+ "                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperShadePanel\" -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"visorPanel\" -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"nodeEditorPanel\" -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n"
		+ "            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -defaultPinnedState 0\n                -ignoreAssets 1\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -keyReleaseCommand \"nodeEdKeyReleaseCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -island 0\n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -syncedSelection 1\n                -extendToShapes 1\n                $editorName;;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -defaultPinnedState 0\n                -ignoreAssets 1\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -keyReleaseCommand \"nodeEdKeyReleaseCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -island 0\n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -syncedSelection 1\n                -extendToShapes 1\n                $editorName;;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"createNodePanel\" -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Texture Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"polyTexturePlacementPanel\" -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"renderWindowPanel\" -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"blendShapePanel\" (localizedPanelLabel(\"Blend Shape\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\tblendShapePanel -unParent -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels ;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tblendShapePanel -edit -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynRelEdPanel\" -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"relationshipPanel\" -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"referenceEditorPanel\" -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"componentEditorPanel\" -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynPaintScriptedPanelType\" -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"scriptEditorPanel\" -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-defaultImage \"\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 1\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"base_OpenGL_Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 1\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"base_OpenGL_Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        setFocus `paneLayout -q -p1 $gMainPane`;\n        sceneUIReplacement -deleteRemaining;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 12 -divisions 5 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 24 -ast 1 -aet 48 ";
	setAttr ".st" 6;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :renderPartition;
	setAttr -s 2 ".st";
select -ne :initialShadingGroup;
	setAttr -s 13 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 13 ".gn";
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultShaderList1;
	setAttr -s 2 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :renderGlobalsList1;
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 18 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surfaces" "Particles" "Fluids" "Image Planes" "UI:" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 18 0 1 1 1 1 1
		 1 0 0 0 0 0 0 0 0 0 0 0 ;
select -ne :defaultHardwareRenderGlobals;
	setAttr ".fn" -type "string" "im";
	setAttr ".res" -type "string" "ntsc_4d 646 485 1.333";
select -ne :ikSystem;
	setAttr -s 4 ".sol";
connectAttr "polySmoothFace1.out" "polySurfaceShape2.i";
connectAttr "groupId12.id" "polySurfaceShape2.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "polySurfaceShape2.iog.og[0].gco";
connectAttr "groupParts5.og" "polySurfaceShape3.i";
connectAttr "groupId13.id" "polySurfaceShape3.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "polySurfaceShape3.iog.og[0].gco";
connectAttr "groupId10.id" "pCylinderShape1.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCylinderShape1.iog.og[0].gco";
connectAttr "groupParts3.og" "pCylinderShape1.i";
connectAttr "groupId11.id" "pCylinderShape1.ciog.cog[0].cgid";
connectAttr "groupId7.id" "pCubeShape1.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCubeShape1.iog.og[0].gco";
connectAttr "groupParts1.og" "pCubeShape1.i";
connectAttr "groupId8.id" "pCubeShape1.ciog.cog[0].cgid";
connectAttr "groupId5.id" "pCubeShape5.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCubeShape5.iog.og[0].gco";
connectAttr "groupId6.id" "pCubeShape5.ciog.cog[0].cgid";
connectAttr "groupId3.id" "pCubeShape6.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCubeShape6.iog.og[0].gco";
connectAttr "groupId4.id" "pCubeShape6.ciog.cog[0].cgid";
connectAttr "groupId1.id" "pCubeShape4.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCubeShape4.iog.og[0].gco";
connectAttr "groupId2.id" "pCubeShape4.ciog.cog[0].cgid";
connectAttr "polySmoothFace2.out" "polySurfaceShape1.i";
connectAttr "groupId9.id" "polySurfaceShape1.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "polySurfaceShape1.iog.og[0].gco";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr ":mentalrayGlobals.msg" ":mentalrayItemsList.glb";
connectAttr ":miDefaultOptions.msg" ":mentalrayItemsList.opt" -na;
connectAttr ":miDefaultFramebuffer.msg" ":mentalrayItemsList.fb" -na;
connectAttr ":miDefaultOptions.msg" ":mentalrayGlobals.opt";
connectAttr ":miDefaultFramebuffer.msg" ":mentalrayGlobals.fb";
connectAttr "polyCylinder1.out" "polySplitRing1.ip";
connectAttr "pCylinderShape1.wm" "polySplitRing1.mp";
connectAttr "polyTweak1.out" "polySplitRing2.ip";
connectAttr "pCylinderShape1.wm" "polySplitRing2.mp";
connectAttr "polySplitRing1.out" "polyTweak1.ip";
connectAttr "polyTweak2.out" "polySplit1.ip";
connectAttr "polySplitRing2.out" "polyTweak2.ip";
connectAttr "polySplit1.out" "polyExtrudeEdge1.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge1.mp";
connectAttr "polyExtrudeEdge1.out" "polyTweak3.ip";
connectAttr "polyTweak3.out" "deleteComponent1.ig";
connectAttr "deleteComponent1.og" "deleteComponent2.ig";
connectAttr "deleteComponent2.og" "polyExtrudeEdge2.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge2.mp";
connectAttr "polyTweak4.out" "polyExtrudeEdge3.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge3.mp";
connectAttr "polyExtrudeEdge2.out" "polyTweak4.ip";
connectAttr "polyTweak5.out" "polyExtrudeEdge4.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge4.mp";
connectAttr "polyExtrudeEdge3.out" "polyTweak5.ip";
connectAttr "polyTweak6.out" "polyExtrudeEdge5.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge5.mp";
connectAttr "polyExtrudeEdge4.out" "polyTweak6.ip";
connectAttr "polyTweak7.out" "polyExtrudeEdge6.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge6.mp";
connectAttr "polyExtrudeEdge5.out" "polyTweak7.ip";
connectAttr "polyTweak8.out" "polyExtrudeEdge7.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge7.mp";
connectAttr "polyExtrudeEdge6.out" "polyTweak8.ip";
connectAttr "polyTweak9.out" "polyExtrudeEdge8.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge8.mp";
connectAttr "polyExtrudeEdge7.out" "polyTweak9.ip";
connectAttr "polyTweak10.out" "polyExtrudeEdge9.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge9.mp";
connectAttr "polyExtrudeEdge8.out" "polyTweak10.ip";
connectAttr "polyTweak11.out" "polyExtrudeEdge10.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge10.mp";
connectAttr "polyExtrudeEdge9.out" "polyTweak11.ip";
connectAttr "polyTweak12.out" "polyExtrudeEdge11.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge11.mp";
connectAttr "polyExtrudeEdge10.out" "polyTweak12.ip";
connectAttr "polyTweak13.out" "polyExtrudeEdge12.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge12.mp";
connectAttr "polyExtrudeEdge11.out" "polyTweak13.ip";
connectAttr "polyTweak14.out" "polyExtrudeEdge13.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge13.mp";
connectAttr "polyExtrudeEdge12.out" "polyTweak14.ip";
connectAttr "polyTweak15.out" "polyExtrudeEdge14.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge14.mp";
connectAttr "polyExtrudeEdge13.out" "polyTweak15.ip";
connectAttr "polyTweak16.out" "polyExtrudeEdge15.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge15.mp";
connectAttr "polyExtrudeEdge14.out" "polyTweak16.ip";
connectAttr "polyTweak17.out" "polyExtrudeEdge16.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge16.mp";
connectAttr "polyExtrudeEdge15.out" "polyTweak17.ip";
connectAttr "polyTweak18.out" "polyExtrudeEdge17.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge17.mp";
connectAttr "polyExtrudeEdge16.out" "polyTweak18.ip";
connectAttr "polyTweak19.out" "polyExtrudeEdge18.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge18.mp";
connectAttr "polyExtrudeEdge17.out" "polyTweak19.ip";
connectAttr "polyTweak20.out" "polyExtrudeEdge19.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge19.mp";
connectAttr "polyExtrudeEdge18.out" "polyTweak20.ip";
connectAttr "polyTweak21.out" "polyExtrudeEdge20.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge20.mp";
connectAttr "polyExtrudeEdge19.out" "polyTweak21.ip";
connectAttr "polyTweak22.out" "polyExtrudeEdge21.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge21.mp";
connectAttr "polyExtrudeEdge20.out" "polyTweak22.ip";
connectAttr "polyTweak23.out" "polyMergeVert1.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert1.mp";
connectAttr "polyExtrudeEdge21.out" "polyTweak23.ip";
connectAttr "polyCube1.out" "polyBevel1.ip";
connectAttr "pCubeShape1.wm" "polyBevel1.mp";
connectAttr "polyBevel1.out" "polySplit2.ip";
connectAttr "polyTweak24.out" "polySplit3.ip";
connectAttr "polySplit2.out" "polyTweak24.ip";
connectAttr "polyTweak25.out" "polyBevel2.ip";
connectAttr "pCubeShape1.wm" "polyBevel2.mp";
connectAttr "polySplit3.out" "polyTweak25.ip";
connectAttr "polyTweak26.out" "polySplit4.ip";
connectAttr "polyBevel2.out" "polyTweak26.ip";
connectAttr "pCubeShape4.o" "polyUnite1.ip[0]";
connectAttr "pCubeShape6.o" "polyUnite1.ip[1]";
connectAttr "pCubeShape5.o" "polyUnite1.ip[2]";
connectAttr "pCubeShape1.o" "polyUnite1.ip[3]";
connectAttr "pCubeShape4.wm" "polyUnite1.im[0]";
connectAttr "pCubeShape6.wm" "polyUnite1.im[1]";
connectAttr "pCubeShape5.wm" "polyUnite1.im[2]";
connectAttr "pCubeShape1.wm" "polyUnite1.im[3]";
connectAttr "polySplit4.out" "groupParts1.ig";
connectAttr "groupId7.id" "groupParts1.gi";
connectAttr "polyUnite1.out" "groupParts2.ig";
connectAttr "groupId9.id" "groupParts2.gi";
connectAttr "polyMergeVert1.out" "polyChipOff1.ip";
connectAttr "pCylinderShape1.wm" "polyChipOff1.mp";
connectAttr "pCylinderShape1.o" "polySeparate1.ip";
connectAttr "polyChipOff1.out" "groupParts3.ig";
connectAttr "groupId10.id" "groupParts3.gi";
connectAttr "polySeparate1.out[0]" "groupParts4.ig";
connectAttr "groupId12.id" "groupParts4.gi";
connectAttr "polySeparate1.out[1]" "groupParts5.ig";
connectAttr "groupId13.id" "groupParts5.gi";
connectAttr "groupParts4.og" "polySmoothFace1.ip";
connectAttr "groupParts2.og" "polySmoothFace2.ip";
connectAttr "pCubeShape4.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape4.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape6.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape6.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape5.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape5.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape1.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape1.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "polySurfaceShape1.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCylinderShape1.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCylinderShape1.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "polySurfaceShape2.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "polySurfaceShape3.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "groupId1.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId2.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId3.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId4.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId5.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId6.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId7.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId8.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId9.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId10.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId11.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId12.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId13.msg" ":initialShadingGroup.gn" -na;
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
// End of fan.ma
