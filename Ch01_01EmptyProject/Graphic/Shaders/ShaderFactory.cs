﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    public enum ShaderName
    {
        Color,
        Ambient,
        Diffuse,
        Specular,
        Texture,
        Test,
        Multitexture,
        Bumpmaping,
        ParallaxMapping,
        LightingEffect,
        DirectionalLightingParallaxMapping,
        DepthShader,
    }

    public static class ShaderFactory
    {
        public static IShaderEffect Create(ShaderName name)
        {
            IShaderEffect shaderEffect;

            switch (name)
            {
                case ShaderName.Color:
                    shaderEffect = new ColorShader();
                    break;
                case ShaderName.Ambient:
                    shaderEffect = new AmbientShader();
                    break;
                case ShaderName.Diffuse:
                    shaderEffect = new DiffuseLighting();
                    break;
                case ShaderName.Specular:
                    shaderEffect = new SpecularLighting();
                    break;
                case ShaderName.Multitexture:
                    shaderEffect = new Multitexturing();
                    break;
                case ShaderName.Texture:
                    shaderEffect = new TextureMapping();
                    break;
                case ShaderName.Test:
                    shaderEffect = new TestShader();
                    break;
                case ShaderName.Bumpmaping:
                    shaderEffect = new BumpMapping();
                    break;
                case ShaderName.ParallaxMapping:
                    shaderEffect = new ParallaxMapping();
                    break;
                case ShaderName.LightingEffect:
                    shaderEffect = new LightingShader();
                    break;
                case ShaderName.DirectionalLightingParallaxMapping:
                    shaderEffect = new DirectionalLightingParallax();
                    break;
                case ShaderName.DepthShader:
                    shaderEffect = new DepthShader();
                    break;
                default:
                    shaderEffect = null;
                    break;
            }
            return shaderEffect;
        }
    }
}
