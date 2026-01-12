# ML-25-26-01-AI-Preprocessing-Agent-Plugin-for-Improving-OCR-Performance-Hidden_Layer
AI-driven preprocessing agent/plugin that adaptively enhances document images (denoise, deskew, normalize, binarize, sharpen, and more) to improve OCR accuracy and entity extraction robustness.

## Project Introduction

OCR-based systems often fail on real-world documents because the input images are not in good shape, as for example,  imbalanced brightness/contrast, skewed perspective, uneven lighting, blur, or noise. When these attributes do not play along,  OCR starts to struggle, and tasks like extracting names, dates, totals, IDs, or key-value fields also degrade.

This project will build an **AI-driven preprocessing tool/plugin** that improves OCR performance by generating **multiple enhanced variants** of a single input image. The tool takes:
1) an image reference (e.g., `123ABC.jpg`), and  
2) a natural language prompt describing what should be improved (e.g., “zoom in from 10% to 50% in steps of 5%”, or “rotate from 0° to 175° in steps of 10°”).

Based on the prompt, the agent decides which image manipulations to apply. Then, in response, it gives the required set of processed images. Each output image is stored in a configurable backend (starting with the local filesystem) and returned with a unique reference ID so that OCR extraction can easily consume it.

## Goals

- Provide a reusable **ai tool/plugin interface** that accepts an image reference + user prompt.
- Interpret the prompt (LLM-based reasoning) and decide which preprocessing to apply.
- Generate multiple output variants.
- Store original + processed images in a configurable storage backend.
- Ensure consistent naming and referencing for easy retrieval and downstream OCR pipelines.

## Functional Scope

### Input
- Accept a single input image by reference (e.g., `123ABC`) rather than only raw file paths.
- Support common formats such as **JPG, PNG, TIFF, BMP**.
- Accept a natural language prompt specifying the desired improvements and ranges.
  
### Prompt interpretation → decision making
Use an LLM to map the prompt into one or more manipulations and parameters.  
At least *5 manipulations* are required (more are a bonus). Examples include:

- Zoom in / zoom out (crop/resize)
- Contrast optimization (auto-contrast, histogram equalization)
- Brightness adjustment
- Sharpening/blur reduction
- Skew correction / deskew (perspective transform)
- Noise reduction (denoising filters)
- Binarization (thresholding)
- Rotation correction
- Background removal

### Image processing output
- Produce multiple images depending on the prompt (e.g., start/end/step).
- Variants may differ by a single manipulation (parameter sweep) or by a sequence of steps.
  ---
### Output + storage
- Return a list of generated image references.
- Store all images (original + processed) in a configurable storage system:
  - default: local filesystem (a folder configured by the user)
- Name files consistently using the reference ID (examples):
  - img1234_original.png
  - img1234_contrast_enhanced.png
  - img1234_zoomed_sharpened.png
  - or short IDs like ABG787.png

## Example tool signature

```csharp
[Description("Performs zooming in of the given image. It returns the comma-separated list of references of generated images.")]
public Task<string> ZoomInImage(
    [Description("The reference number of the image, which should be zoomed in.")] string imageReference,
    [Description("The first zoom in in percent.")] int startZoom,
    [Description("The last zoom in in percent")] int endZoom,
    [Description("The delta zoom, which defines zoom-in step.")] int zoomStep)
{
    throw new NotImplementedException();
}
```

## Project Members
- Baha Uddin Ahmed -   Matriculation No: 1568217
- MD Tazmim Hossain - Matriculation No: 1502556
- Rakat Murshed  — Matriculation No: 1502837
- M M Rauf Shahriyar -  Matriculation No: 1504901
