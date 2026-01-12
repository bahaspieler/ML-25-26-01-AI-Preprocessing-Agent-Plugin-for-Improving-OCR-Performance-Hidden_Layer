# Project Plan (v0.1)
## Project Title
AI Preprocessing Agent/Plugin for Improving OCR Performance

## Objective
Build a reusable **AI agent tool/plugin** in **.NET (C#)**. This agent takes an **image reference** and a **natural-language prompt** to generate **multiple enhanced image variants** by mapping user prompts and preprocessing tools/plugins. Then stores all output in a configurable backend, and returns a list of **new image references**. The new images can be easily accessible for further OCR and downstream entity extraction.

The project will satisfy all functional requirements and additionally provide practical workflows for common document types (e.g., receipts, invoices, bank statements) and a benchmarking tool.



## Mandatory Repository Structure
The repository root must contain:

- A **single solution file** (`.sln`)
- A **Documentation/** folder
- A **main project folder** (e.g., `OCRAgent/`)
- A **UnitTests/** folder containing the unit test project

**Example root layout:**
```
OCRAgent.sln
Documentation/
OCRAgent/
UnitTests/
README.md
```

## Scope

### In Scope (Must-Have)
- Input by **reference ID** (e.g., `123ABC`) and support for JPG/PNG/TIFF/BMP.
- Natural-language prompt interpretation using OpenAI to select image-processing plugins and parameter ranges.
- Implementation of **at least 5** preprocessing techniques (more for bonus).
- Generation of multiple variants (start/end/step).
- Storage of original + processed images in a configurable store (default: local file system).
- Consistent naming and reference IDs for retrieval and downstream OCR use.
- CLI runner and a minimal logging/trace per run.
- Automated tests in `UnitTests/` and a buildable solution.

### Extended Scope
- OCR integration (baseline engine) and automatic “best variant” selection.
- Document workflows:
  - Receipts: merchant, date, total, tax, currency
- Benchmark harness: runtime, number of variants, OCR quality proxy metrics, and simple plan-correctness scoring.

## Design Overview

### Key Components
1. *Storage Layer*
   - IImageStore: save/load/list metadata for images by reference.
   - LocalFileImageStore: saves images in a designated directory; can optionally generate a separate .json file containing metadata for each image.

2. *Preprocessing Tools (Plugin)*
   - Fundamental tools (e.g., rotate, zoom, binarize, denoise, contrast).
   - Each tool:
     - loads input via reference
     - validates parameters
     - generates 1...N output images
     - stores results and returns output references

3. *Agent Orchestrator*
   - Translates user prompts into a structured, actionable plan consisting of a sequenced set of tools and corresponding parameter ranges.
   - Executes tools and records a run trace (selected tools, parameters, outputs, timings, warnings).

4. *OCR Adapter (Optional in early sprints)*
   - IOcrEngine interface with at least one baseline engine (Tesseract, Google Vision integrations).
   - Utilized to evaluate different image variants and to extract data for workflows.

5. *Workflow Layer (Receipts / Others)*
   - Runs OCR on the best variant, parses the results, and exports structured data.

6. *Benchmarking Tools*
   - Compares baseline vs agent-enhanced results.
   - Produces a compact report (JSON + CSV summary).
   - Workflow benchmarking


## Sprint Plan and Backlog

### Sprint 1 — Project Setup and Image Reference Storage

**Goal:** Initialize project structure and build image registration, storage, and retrieval by reference.

**Backlog**
- Create solution file (`OCRAgent.sln`) at root.
- Add main project in `OCRAgent/`.
- Add MSTest project in `UnitTests/` and reference main project.
- Set up `Documentation/` with:
  - `PROJECT_PLAN.md`
- Implement `IImageStore`:
  - `Save(bytes, meta) -> reference`
  - `Load(reference) -> bytes`
  - `Exists(reference)`
  - `GetMeta(reference)`
- Implement `LocalFileImageStore` with:
  - Configurable storage directory
  - File naming based on reference
  - Metadata stored as sidecar file
- Write tests for:
  - Save/load consistency (hash check)
  - Handling of missing references

**Acceptance Criteria**
- `dotnet build` and `dotnet test` succeed.
- Required folders and documents are present.
- Images can be registered, stored, and retrieved by reference ID.
- Errors are handled cleanly.

---

### Sprint 2 — Preprocessing Toolbox (Core Functionalities)
**Goal:** Implement the core preprocessing tools required for this project.

**Backlog**
Implement at least 5 required basic tools:

**Core Tools (minimum required to meet functional specification)**
- Zoom in/out (start/end/step, cap variants)
- Rotation (start/end/step)
- Contrast optimization
- Brightness adjustment
- Denoise (median/bilateral/NLM)
- Binarization (Otsu/adaptive)
- Deskew
- Sharpening / blur reduction
- Resize/DPI normalization

General tasks:
- Parameter validation and cap on total output variants (e.g., 20)
- Follow consistent naming convention for output references
- Write unit tests for each tool

**Acceptance Criteria**
- Core tools implemented and output images saved with reference IDs.
- Output variants are capped.
- Each tool has unit tests.

---
### Sprint 3 — Preprocessing Toolbox (Extended & Advanced)
*Goal:* Extend toolbox with additional, recommended preprocessing tools to increase robustness and flexibility.

*Backlog*
Implement additional enhancements beyond minimum set:

*Additional / Extended Tools (recommended)*
- Perspective correction (basic)
- Background removal
- Morphology operations (opening/closing)
- and more

General tasks:
- Add and update parameter validation for all extended tools
- Add unit tests for new tools

*Acceptance Criteria*
- Each extended tool works, returns output reference IDs, and passes its tests.
- Both core and advanced tools have complete unit test coverage.

---
