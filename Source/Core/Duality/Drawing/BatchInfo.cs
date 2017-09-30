﻿using System;
using System.Collections.Generic;
using System.Linq;

using Duality.Editor;
using Duality.Resources;

namespace Duality.Drawing
{
	/// <summary>
	/// BatchInfos describe how an object, represented by a set of vertices, looks like.
	/// </summary>
	/// <seealso cref="Material"/>
	public class BatchInfo : IEquatable<BatchInfo>
	{
		private ContentRef<DrawTechnique> technique  = DrawTechnique.Mask;
		private ColorRgba                 mainColor  = ColorRgba.White;
		private ShaderParameters          parameters = null;
		
		/// <summary>
		/// [GET / SET] The <see cref="Duality.Resources.DrawTechnique"/> that is used.
		/// </summary>
		public ContentRef<DrawTechnique> Technique
		{
			get { return this.technique; }
			set { this.technique = value; }
		}
		/// <summary>
		/// [GET] The collection of shader parameters that will be used when setting up 
		/// a shader program for rendering with this <see cref="BatchInfo"/>.
		/// </summary>
		public ShaderParameters Parameters
		{
			get { return this.parameters; }
		}
		/// <summary>
		/// [GET / SET] The main color, typically used for coloring displayed vertices.
		/// </summary>
		public ColorRgba MainColor
		{
			get { return this.mainColor; }
			set { this.mainColor = value; }
		}
		/// <summary>
		/// [GET / SET] The main texture.
		/// </summary>
		public ContentRef<Texture> MainTexture
		{
			get { return this.parameters.MainTexture; }
			set { this.parameters.MainTexture = value; }
		}

		/// <summary>
		/// Creates a new, empty BatchInfo.
		/// </summary>
		public BatchInfo()
		{
			this.parameters = new ShaderParameters();
		}
		/// <summary>
		/// Creates a new BatchInfo based on an existing <see cref="Material"/>.
		/// </summary>
		/// <param name="source"></param>
		public BatchInfo(Material source) : this(source.Info) {}
		/// <summary>
		/// Creates a new BatchInfo based on an existing BatchInfo. This is essentially a copy constructor.
		/// </summary>
		/// <param name="source"></param>
		public BatchInfo(BatchInfo source)
		{
			this.technique = source.technique;
			this.mainColor = source.mainColor;
			this.parameters = new ShaderParameters(source.parameters);
		}
		/// <summary>
		/// Creates a new color-only BatchInfo.
		/// </summary>
		/// <param name="technique">The <see cref="Duality.Resources.DrawTechnique"/> to use.</param>
		/// <param name="mainColor">The <see cref="MainColor"/> to use.</param>
		public BatchInfo(ContentRef<DrawTechnique> technique, ColorRgba mainColor) : this()
		{
			this.technique = technique;
			this.mainColor = mainColor;
		}
		/// <summary>
		/// Creates a new single-texture BatchInfo.
		/// </summary>
		/// <param name="technique">The <see cref="Duality.Resources.DrawTechnique"/> to use.</param>
		/// <param name="mainColor">The <see cref="MainColor"/> to use.</param>
		/// <param name="mainTex">The main <see cref="Duality.Resources.Texture"/> to use.</param>
		public BatchInfo(ContentRef<DrawTechnique> technique, ColorRgba mainColor, ContentRef<Texture> mainTex) : this() 
		{
			this.technique = technique;
			this.mainColor = mainColor;
			this.MainTexture = mainTex;
		}

		public override string ToString()
		{
			return string.Format("{0} ({1})",
				this.parameters, 
				this.technique.Name);
		}
		public override int GetHashCode()
		{
			int hashCode = 17;
			unchecked
			{
				hashCode = hashCode * 23 + this.technique.GetHashCode();
				hashCode = hashCode * 23 + this.mainColor.GetHashCode();
				hashCode = hashCode * 23 + this.parameters.GetHashCode();
			}
			return hashCode;
		}
		public override bool Equals(object obj)
		{
			BatchInfo other = obj as BatchInfo;
			if (other != null)
				return this.Equals(other);
			else
				return false;
		}
		public bool Equals(BatchInfo other)
		{
			return
				this.technique == other.technique &&
				this.mainColor == other.mainColor &&
				this.parameters.Equals(other.parameters);
		}
	}
}
